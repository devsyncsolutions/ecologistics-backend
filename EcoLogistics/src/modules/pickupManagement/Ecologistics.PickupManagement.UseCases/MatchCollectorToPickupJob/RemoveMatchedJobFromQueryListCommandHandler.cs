namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class RemoveMatchedJobFromQueryListCommandHandler(
    IPickupManagementDbContext dbContext)
{
    public async Task<RemoveMatchedJobFromQueryListCompletedCommand> Handle(
        RemoveMatchedJobFromQueryListCommand command, CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(command);

        int affectedRows = await dbContext
                .PickupsForQuerying
                .Where(p => p.PickupId == command.PickupId.Value)
                .ExecuteDeleteAsync(cancellationToken)
                .ConfigureAwait(false);

        return affectedRows == 0
            ? throw new NotFoundException($"Pickup job for query with PickupId {command.PickupId.Value} was not found")
            : new RemoveMatchedJobFromQueryListCompletedCommand(
                command.SagaId,
                command.PickupId,
                command.PickupReference,
                command.TriggeredBy,
                command.UserId);
    }
}
