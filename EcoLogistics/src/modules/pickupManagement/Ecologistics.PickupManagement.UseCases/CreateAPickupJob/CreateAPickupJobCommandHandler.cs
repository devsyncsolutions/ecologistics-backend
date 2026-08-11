// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Exceptions;

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

        Pickup newPickup = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<CreateAPickupJobCommand>(command.RequestId),
            async _ => await CreateNewPickupAsync(dbContext, bus, loggedInUserService, command, cache, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            },
            cancellationToken).ConfigureAwait(false);

        return new CreateAPickupJobResponse(newPickup.Id.Value);
    }

    private static async Task<Pickup> CreateNewPickupAsync(
        IPickupManagementDbContext dbContext,
        IMessageBus bus,
        ILoggedInUserService loggedInUserService,
        CreateAPickupJobCommand command,
        IFusionCache cache,
        CancellationToken cancellationToken)
    {
        // check if user is verified
        bool isUserVerified = await bus
            .InvokeAsync<bool>(new CheckIfUserIsVerifiedQuery(loggedInUserService.GetUserId()), cancellationToken)
            .ConfigureAwait(false);

        if (!isUserVerified)
        {
            await cache
                .ExpireAsync(CacheKeys.GetRequestKey<CreateAPickupJobCommand>(command.RequestId), token: cancellationToken)
                .ConfigureAwait(false);
            throw new ForbiddenRequestException("User is not verified.");
        }

        PickupFeeId pickupFeeId = new(Guid.Parse(command.PickupFeeId));

        bool  pickupFeeExists = await dbContext.PickupFees
            .AnyAsync(pf => pf.Id == pickupFeeId, cancellationToken)
            .ConfigureAwait(false);
        if (!pickupFeeExists)
            throw new NotFoundException("Specified Pickup fee not found.");

        string reference = await GenerateReference(dbContext, command.FormattedAddress, cancellationToken).ConfigureAwait(false);
        var newPickup = Pickup.Create(
            pickupFeeId,
            Enum.Parse<PickupType>(command.Type),
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
        return newPickup;
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
