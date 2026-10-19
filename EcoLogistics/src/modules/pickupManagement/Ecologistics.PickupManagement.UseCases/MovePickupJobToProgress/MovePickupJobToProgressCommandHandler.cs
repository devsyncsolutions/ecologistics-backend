// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace Ecologistics.PickupManagement.UseCases.MovePickupJobToProgress;

public class MovePickupJobToProgressCommandHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache)
{
    public async Task Handle(MovePickupJobToProgressCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync(
            CacheKeys.MovePickupToProgress(command.PickupJobId),
            async _=> await RetrieveAndMovePickupToProgressAsync(dbContext, command, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromHours(4),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(5)
            },
            cancellationToken).ConfigureAwait(false);
    }

    private static async Task<Guid> RetrieveAndMovePickupToProgressAsync(
        IPickupManagementDbContext dbContext,
        MovePickupJobToProgressCommand command,
        CancellationToken cancellationToken)
    {
        Pickup pickup = await dbContext.Pickups
            .AsTracking()
            .Where(p => p.Id == new PickupId(command.PickupJobId))
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);

        pickup.MoveToProgress();
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return pickup.Id.Value;
    }
}
