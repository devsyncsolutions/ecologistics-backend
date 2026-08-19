namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class SendTextMessagesForMatchedJobCommandHandler(
    IPickupManagementDbContext dbContext
    //ITextMessagingService textMessagingService
)
{
    public async Task<SendTextMessagesForMatchedJobCompletedCommand> Handle(SendTextMessagesForMatchedJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var pickup = await dbContext
            .Pickups
            .Where(p => p.Id == command.PickupId)
            .Select(p => new
            {
                p.Id
            })
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return new SendTextMessagesForMatchedJobCompletedCommand(command.SagaId, pickup.Id);
    }
}