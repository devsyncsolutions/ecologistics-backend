// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetActivePickupAsCollector;

public record GetActivePickupAsCollectorQuery;

public class GetActivePickupAsCollectorQueryHandler(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task<ActivePickupDto?> Handle(
        GetActivePickupAsCollectorQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        string userId = loggedInUserService.GetUserId();

        PickupStatus[] activeStatuses =
        [
            PickupStatus.Started,
            PickupStatus.Matched,
            PickupStatus.InProgress,
            PickupStatus.CollectorEnRoute
        ];

        ActivePickupDto? activeJob = await dbContext
            .Pickups
            .Where(p =>
                p.CollectorMemberId == userId &&
                activeStatuses.Contains(p.PickupStatus))
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ActivePickupDto(
                PickupId: p.Id.Value.ToString(),
                ContentName: p.WasteContentName,
#pragma warning disable CA1305
                StartedAt: p.TimeStamp.JobStartedAt.ToString()!,
                Status: p.PickupStatus.ToString(),
                Reference: p.Reference))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
#pragma warning restore CA1305

        if(activeJob is null)
            throw new NotFoundException($"No active pickup job found for collector with ID: {userId}");

        return activeJob;
    }
}
