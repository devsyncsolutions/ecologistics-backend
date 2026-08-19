namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class RemoveMatchedJobFromQueryListCommandHandler(
    IPickupManagementDbContext dbContext,
    ILogger<RemoveMatchedJobFromQueryListCommandHandler> logger)
{
    public async Task<RemoveMatchedJobFromQueryListCompletedCommand> Handle(
        RemoveMatchedJobFromQueryListCommand command, CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(command);

        bool pickupExists = await dbContext
            .PickupsForQuerying
            .AnyAsync(p => p.PickupId == command.PickupId.Value, cancellationToken)
            .ConfigureAwait(false);

        if (!pickupExists)
        {
            logger.LogErrorMessage(
                $"Error occured in a RemoveMatchedJobFromQueryListCommandHandler. Error: Pickup for query with pickup id {command.PickupId.Value} does not exist.",
                null,
                null,
                null
            );

            throw new ArgumentException("Pickup for query with pickup id {command.PickupId.Value} does not exist.");
        }

        await dbContext
                .PickupsForQuerying
                .Where(p => p.PickupId == command.PickupId.Value)
                .ExecuteDeleteAsync(cancellationToken)
                .ConfigureAwait(false);

        return new RemoveMatchedJobFromQueryListCompletedCommand(command.SagaId, command.PickupId);
    }
}