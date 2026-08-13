// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public class CreateAPickupJobCommandHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache,
    IMessageBus bus,
    ILoggedInUserService loggedInUserService)
{
    public async Task<CreateAPickupJobResponse> Handle(CreateAPickupJobCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var validation = new CreateAPickupJobCommandValidator();
        var validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Guid newPickupId = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<CreateAPickupJobCommand>(command.RequestId),
            async _ => await CreateNewPickupAsync(dbContext, bus, loggedInUserService, command, cancellationToken).ConfigureAwait(false),
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
        IMessageBus bus,
        ILoggedInUserService loggedInUserService,
        CreateAPickupJobCommand command,
        CancellationToken cancellationToken)
    {
        // check if user is verified
        bool isUserVerified = await bus
            .InvokeAsync<bool>(new CheckIfUserIsVerifiedQuery(loggedInUserService.GetUserId()), cancellationToken)
            .ConfigureAwait(false);

        if (!isUserVerified)
            throw new ForbiddenRequestException("User is not verified.");

        PickupFeeId pickupFeeId = new(Guid.Parse(command.PickupFeeId));
        DistrictId districtId = new(Guid.Parse(loggedInUserService.GetDistrictId()));

        bool  pickupFeeExists = await dbContext.PickupFees
            .AnyAsync(pf => pf.Id == pickupFeeId && pf.DistrictId == districtId, cancellationToken)
            .ConfigureAwait(false);

        if (!pickupFeeExists)
            throw new NotFoundException("Specified Pickup fee not found.");

        var type = Enum.Parse<PickupType>(command.Type);

        string reference = await GenerateReference(dbContext, command.FormattedAddress, cancellationToken).ConfigureAwait(false);
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
            command.DaysIntervalForPickupIfRecurring);

        await dbContext.Pickups.AddAsync(newPickup, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return newPickup.Id.Value;
    }

    private static async Task<string> GenerateReference(IPickupManagementDbContext dbContext, string formattedAddress, CancellationToken cancellationToken)
    {
        bool alreadyUsed = true;
        string reference = "";

        while (alreadyUsed)
        {
            string initial = formattedAddress.Length >= 2 ? formattedAddress[..2] : formattedAddress;
            reference = $"{initial.ToUpper()}-{CacheKeys.GenerateARegistrationNumber(4)}";
            alreadyUsed = await dbContext.Pickups
                .AnyAsync(p => p.Reference == reference, cancellationToken).ConfigureAwait(false);
        }

        return reference;
    }
}
