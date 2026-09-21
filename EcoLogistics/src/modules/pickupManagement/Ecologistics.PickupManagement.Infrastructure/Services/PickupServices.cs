// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Services;

public class PickupServices(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IMessageBus bus) : IPickupServices
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
        Task<decimal> districtTask = bus.InvokeAsync<decimal>(
            new GetDistrictBalanceQuery(Guid.Parse(loggedInUserService.GetUserId())), cancellationToken);
        Task<decimal> tenantTask = bus.InvokeAsync<decimal>(
            new GetTenantBalanceQuery(), cancellationToken);

        decimal balance = await districtTask.ConfigureAwait(false);
        decimal tenantBalance = await tenantTask.ConfigureAwait(false);

        if(tenantBalance > balance)
            return (false, "Your tenant balance is above the least threshold to accept a pickup job. Please pay off your owned balance.");

        bool alreadyMatchedWithAnActiveJob = await dbContext
            .Pickups
            .Where(p =>
                p.CollectorMemberId == loggedInUserService.GetUserId() &&
                ActiveStatuses.Contains(p.PickupStatus))
            .AnyAsync(cancellationToken)
            .ConfigureAwait(false);

        if(alreadyMatchedWithAnActiveJob)
            return (false, "You are already matched with an active pickup job. Please complete it before accepting a new one.");

        return (true, null);
    }

    private static readonly PickupStatus[] ActiveStatuses =
    [
        PickupStatus.Started,
        PickupStatus.Matched,
        PickupStatus.InProgress,
        PickupStatus.CollectorEnRoute
    ];
}
