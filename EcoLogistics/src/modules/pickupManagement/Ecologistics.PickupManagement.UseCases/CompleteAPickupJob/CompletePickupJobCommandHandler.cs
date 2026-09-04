// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CompleteAPickupJob;

public class CompletePickupJobCommandHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache,
    ILoggedInUserService loggedInUserService)
{
    public async Task Handle(CompletePickupJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<CompletePickupJobCommand>(command.RequestId),
            async _ => await CompleteAsync(command, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            },
            cancellationToken).ConfigureAwait(false);
    }

    private async Task<int> CompleteAsync(CompletePickupJobCommand command, CancellationToken cancellationToken)
    {
        Pickup? pickup = await dbContext.Pickups
            .AsTracking()
            .Where(p => p.Id == new PickupId(command.PickupId))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickup is null)
            throw new NotFoundException($"Pickup with Id {command.PickupId} was not found");

        // check for payment status for the job before completing it

        pickup.Complete(loggedInUserService.GetUserId());
        return await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
