// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace Ecologistics.PickupManagement.UseCases.StartAScheduledJob;

public class StartAScheduledJobCommandHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache,
    ILoggedInUserService loggedInUserService)
{
    public async Task Handle(StartAScheduledJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<StartAScheduledJobCommand>(command.RequestId),
            async _ => await StartPickupAsync(command, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            },
            cancellationToken).ConfigureAwait(false);
    }

    private async Task<Guid> StartPickupAsync(StartAScheduledJobCommand command, CancellationToken cancellationToken)
    {
        Pickup? pickup = await dbContext.Pickups
            .AsTracking()
            .Where(p => p.Id == new PickupId(command.PickupId))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickup is null)
            throw new NotFoundException("Pickup not found.");

        pickup.Start(loggedInUserService.GetUserId());
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return pickup.Id.Value;
    }
}
