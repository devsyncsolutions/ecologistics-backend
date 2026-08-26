// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CollectorIsAssignedToPickupJob;

public class CollectorIsAssignedToPickupJobQueryHandler(
    IPickupManagementDbContext dbContext, IFusionCache cache)
{
    public async Task<bool> Handle(CollectorIsAssignedToPickupJobQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await cache.GetOrSetAsync(
            CacheKeys.IsAssignedToJobKey(query.PickupJobId, query.UserId),
            async _ => await CheckIfCollectorIsAssignedToPickupJobAsync(query.PickupJobId, query.UserId, dbContext, cancellationToken)
            .ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromHours(4),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(5)
            },
            cancellationToken)
            .ConfigureAwait(false);
    }

    private static async Task<bool> CheckIfCollectorIsAssignedToPickupJobAsync(Guid pickupJobId, string collectorId,
        IPickupManagementDbContext dbContext, CancellationToken cancellationToken)
        => await dbContext.Pickups
            .AnyAsync(p => p.Id == new PickupId(pickupJobId) && p.CollectorMemberId == collectorId, cancellationToken)
            .ConfigureAwait(false);
}
