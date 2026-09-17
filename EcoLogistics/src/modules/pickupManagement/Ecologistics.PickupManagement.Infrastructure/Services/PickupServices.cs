// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Services;

public class PickupServices(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService) : IPickupServices
{
    public async Task<string> GenerateReference(string formattedAddress, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(formattedAddress);

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

    public async Task<(bool, string?)> IsEligibleToAcceptAPickup(CancellationToken cancellationToken)
    {
        PickupStatus[] activeStatuses =
        [
            PickupStatus.Started,
            PickupStatus.Matched,
            PickupStatus.InProgress,
            PickupStatus.CollectorEnRoute
        ];

        bool alreadyMatchedWithAnActiveJob = await dbContext
            .Pickups
            .AsTracking()
            .Where(p =>
                p.CollectorMemberId == loggedInUserService.GetUserId() &&
                activeStatuses.Contains(p.PickupStatus))
            .AnyAsync(cancellationToken)
            .ConfigureAwait(false);

        if(alreadyMatchedWithAnActiveJob)
            return (false, "You are already matched with an active pickup job. Please complete it before accepting a new one.");

        return (true, null);
    }
}
