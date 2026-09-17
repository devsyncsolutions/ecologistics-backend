namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class MatchCollectorToPickupJobCommandHandler(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IFusionCache cache,
    IPickupServices pickupServices)
{
    public async Task Handle(MatchCollectorToPickupJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync<string>(
            CacheKeys.GetRequestKey<MatchCollectorToPickupJobCommand>(command.RequestId),
            async _=> await MatchPickupAsync(dbContext, loggedInUserService, pickupServices, new PickupId(command.PickupJobId)
                    , cancellationToken)
                .ConfigureAwait(false),
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
        IPickupServices pickupServices,
        PickupId pickupId,
        CancellationToken cancellationToken
    )
    {
        (bool isEligible, string? error) = await pickupServices.IsEligibleToAcceptAPickup(cancellationToken).ConfigureAwait(false);

        if (!isEligible)
            throw new ForbiddenRequestException(error!);

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
