// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Services;

public class PickupServices(
    PickupManagementDbContext dbContext,
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

    public async Task<EligibilityResult> IsEligibleToAcceptAPickup(CancellationToken cancellationToken)
    {
        string districtId = loggedInUserService.GetDistrictId();
        Task<decimal> districtTask = bus.InvokeAsync<decimal>(
            new GetDistrictBalanceQuery(Guid.Parse(districtId)), cancellationToken);
        Task<decimal> tenantTask = bus.InvokeAsync<decimal>(
            new GetTenantBalanceQuery(), cancellationToken);

        decimal balance = await districtTask.ConfigureAwait(false);
        decimal tenantBalance = await tenantTask.ConfigureAwait(false);

        if(tenantBalance > balance)
            return new EligibilityResult(false, "Your tenant credit is above the least threshold to accept a pickup job. Please pay off your owned credit.");

        bool alreadyMatchedWithAnActiveJob = await dbContext
            .Pickups
            .Where(p =>
                p.CollectorMemberId == loggedInUserService.GetUserId() &&
                ActiveStatuses.Contains(p.PickupStatus))
            .AnyAsync(cancellationToken)
            .ConfigureAwait(false);

        if(alreadyMatchedWithAnActiveJob)
            return new EligibilityResult(false, "You are already matched with an active pickup job. Please complete it before accepting a new one.");

        return new EligibilityResult(true, null);
    }

    public async Task<List<PickupHistoryAsCollectorDto>> GetPickupHistoryAsCollector(int page, int pageSize,
        PickupType? type, Priority? priority, string? wasteContentName,
        DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        string collectorId = loggedInUserService.GetUserId();
        const string completedStatus = nameof(PickupStatus.Completed);
        string? typeStr = type?.ToString();
        string? priorityStr = priority?.ToString();
        int offset = (page - 1) * pageSize;

        List<PickupHistoryAsCollectorDto> history = await dbContext.Database
            .SqlQuery<PickupHistoryAsCollectorDto>(
                $"""
                SELECT
                    CAST(p."Id" AS TEXT)            AS "PickupId",
                    p."Reference",
                    p."WasteContentName",
                    CAST(p."JobCompletedAt" AS TEXT) AS "CompletedAt",
                    pji."AmountPayable"             AS "Amount",
                    pji."Commission"
                FROM "Pickups" p
                INNER JOIN "PickupJobInvoices" pji ON pji."AssociatedPickupId" = p."Id"
                WHERE p."IsDeleted" = false
                  AND p."CollectorMemberId" = {collectorId}
                  AND p."PickupStatus" = {completedStatus}
                  AND p."JobCompletedAt" >= {fromDate}
                  AND p."JobCompletedAt" <= {toDate}
                  AND ({typeStr} IS NULL OR p."Type" = {typeStr})
                  AND ({priorityStr} IS NULL OR p."Priority" = {priorityStr})
                  AND ({wasteContentName} IS NULL OR p."WasteContentName" ILIKE '%' || {wasteContentName} || '%')
                ORDER BY p."JobCompletedAt" DESC
                LIMIT {pageSize} OFFSET {offset}
                """)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return history;
    }

    private static readonly PickupStatus[] ActiveStatuses =
    [
        PickupStatus.Started,
        PickupStatus.Matched,
        PickupStatus.InProgress,
        PickupStatus.CollectorEnRoute
    ];
}
