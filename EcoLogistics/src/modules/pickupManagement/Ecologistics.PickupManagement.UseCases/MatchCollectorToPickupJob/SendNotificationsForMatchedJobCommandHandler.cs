namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class SendNotificationsForMatchedJobCommandHandler(IMessageBus bus )
{
    public async Task<SendNotificationsForMatchedJobCompletedCommand> Handle(SendNotificationsForMatchedJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        string title = "Pickup matched";
        string body = $"Your pickup with Reference {command.PickupReference} has been matched with a collector.";

        await bus.PublishAsync(new CreateNotificationCommand(
            TriggeredBy: command.TriggeredBy,
            UserId: command.UserId,
            Title: title,
            Body: body
        ))
        .ConfigureAwait(false);

        return new SendNotificationsForMatchedJobCompletedCommand(
            command.SagaId, command.Id, command.PickupReference, command.TriggeredBy, command.UserId);
    }
}
