// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public class CreateAPickupJobCommandHandler(
    IPickupManagementDbContext dbContext,
    IPickupServices pickupServices,
    IFusionCache cache,
    ILoggedInUserService loggedInUserService)
{
    public async Task<CreateAPickupJobResponse> Handle(CreateAPickupJobCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        Guid newPickupId = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<CreateAPickupJobCommand>(command.RequestId),
            async _ => await CreateNewPickupAsync(dbContext, loggedInUserService, pickupServices, command, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            },
            cancellationToken).ConfigureAwait(false);

        return new CreateAPickupJobResponse(newPickupId);
    }

    private static async Task<Guid> CreateNewPickupAsync(
        IPickupManagementDbContext dbContext,
        ILoggedInUserService loggedInUserService,
        IPickupServices pickupServices,
        CreateAPickupJobCommand command,
        CancellationToken cancellationToken)
    {
        var validation = new CreateAPickupJobCommandValidator();
        ValidationResult validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Restrict the number of uncompleted jobs to be created in code
        // move to settings table later
        int numberOfActiveJobs = await dbContext.Pickups
            .Where(p => p.CreatedBy == loggedInUserService.GetUserId() && p.PickupStatus != PickupStatus.Completed)
            .CountAsync(cancellationToken).ConfigureAwait(false);

        if (numberOfActiveJobs == 3)
            throw new ForbiddenRequestException("You've reached the maximum number of active jobs.");

        PickupFeeId pickupFeeId = new(Guid.Parse(command.PickupFeeId));
        DistrictId districtId = new(Guid.Parse(loggedInUserService.GetDistrictId()));

        bool  pickupFeeExists = await dbContext.PickupFees
            .AnyAsync(pf => pf.Id == pickupFeeId && pf.DistrictId == districtId, cancellationToken)
            .ConfigureAwait(false);

        if (!pickupFeeExists)
            throw new NotFoundException("Specified Pickup fee not found.");

        var type = Enum.Parse<PickupType>(command.Type);

        string reference = await pickupServices.GenerateReference(command.FormattedAddress, cancellationToken).ConfigureAwait(false);
        var newPickup = Pickup.Create(
            pickupFeeId,
            type,
            command.Coordinates.Latitude,
            command.Coordinates.Longitude,
            command.FormattedAddress,
            command.GhanaPostGpsAddress ?? string.Empty,
            command.Landmark,
            command.ContactName,
            command.ContactPhone,
            command.ExtraNotes,
            DateTimeOffset.Parse(command.ScheduledForPickupAt, CultureInfo.InvariantCulture),
            command.WasteContentName,
            Enum.Parse<Priority>(command.Priority),
            reference,
            command.DaysIntervalForPickupIfRecurring,
            command.NumberOfContainers);

        await dbContext.Pickups.AddAsync(newPickup, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return newPickup.Id.Value;
    }
}
