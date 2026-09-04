namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class MatchCollectorToPickupJobCommandHandler(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IFusionCache cache)
{
    public async Task Handle(MatchCollectorToPickupJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync<string>(
            CacheKeys.GetRequestKey<MatchCollectorToPickupJobCommand>(command.RequestId),
            async _=> await MatchPickupAsync(dbContext, loggedInUserService, new PickupId(command.PickupJobId), cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            },
            cancellationToken).ConfigureAwait(false);
    }

    private static async Task<string> MatchPickupAsync(
        IPickupManagementDbContext dbContext,
        ILoggedInUserService loggedInUserService,
        PickupId pickupId,
        CancellationToken cancellationToken
    )
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

        if (alreadyMatchedWithAnActiveJob)
            throw new ForbiddenRequestException("You've already matched with an active job.");

        Pickup? existingPickupJob = await dbContext
            .Pickups
            .AsTracking()
            .Where(p => p.Id == pickupId)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (existingPickupJob is null)
            throw new NotFoundException("Pickup job was not found");

        existingPickupJob.Match(loggedInUserService.GetUserId());
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return pickupId.Value.ToString();
    }
}
