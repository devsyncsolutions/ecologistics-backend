// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GeneratorCanTrackPickupJob;

public class GeneratorCanTrackPickupJobQueryHandler(
    IPickupManagementDbContext dbContext, IFusionCache cache)
{
    public async Task<bool> Handle(GeneratorCanTrackPickupJobQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await cache.GetOrSetAsync(
                CacheKeys.CanTrackJobKey(query.PickupJobId, query.UserId),
                async _ => await CheckIfGeneratorCanTrackJobAsync(query.PickupJobId, query.UserId, dbContext, cancellationToken).ConfigureAwait(false),
                new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromHours(4),
                    IsFailSafeEnabled = true,
                    FailSafeMaxDuration = TimeSpan.FromHours(5)
                },
                cancellationToken)
            .ConfigureAwait(false);
    }

    private static async Task<bool> CheckIfGeneratorCanTrackJobAsync(Guid pickupJobId, string userId,
        IPickupManagementDbContext dbContext, CancellationToken cancellation)
        => await dbContext.Pickups
            .AnyAsync(p => p.Id == new PickupId(pickupJobId) && p.CreatedBy == userId, cancellation)
            .ConfigureAwait(false);
}
