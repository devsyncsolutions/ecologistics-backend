// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CancelPickupJob;

public class CancelPickupJobCommandHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache,
    ILoggedInUserService loggedInUserService)
{
    public async Task Handle(CancelPickupJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<CancelPickupJobCommand>(command.RequestId),
            async _ => await CancelAsync(command, dbContext, loggedInUserService, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            },
            cancellationToken)
            .ConfigureAwait(false);
    }

    private static async Task<int> CancelAsync(
        CancelPickupJobCommand command,
        IPickupManagementDbContext dbContext,
        ILoggedInUserService loggedInUserService,
        CancellationToken cancellationToken)
    {
        Pickup? pickup = await dbContext
            .Pickups
            .AsTracking()
            .Where(p => p.Id == new PickupId(command.PickupId))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if(pickup is null)
            throw new NotFoundException($"Pickup with Id {command.PickupId} was not found");

        pickup.Cancel(command.Reason, loggedInUserService.GetUserId());

        return await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
