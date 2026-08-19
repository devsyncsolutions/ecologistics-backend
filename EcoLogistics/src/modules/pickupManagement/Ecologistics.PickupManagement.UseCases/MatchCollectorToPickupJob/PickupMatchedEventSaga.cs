namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public record RemoveMatchedJobFromQueryListCompletedCommand(
    [property: SagaIdentity] Guid SagaId, 
    PickupId PickupId);

public record SendNotificationsForMatchedJobCommand(
    [property: SagaIdentity] Guid SagaId, 
    PickupId Id);

public record SendNotificationsForMatchedJobCompletedCommand(
    [property: SagaIdentity] Guid SagaId, 
    PickupId PickupId
);

public record SendTextMessagesForMatchedJobCommand(
    [property: SagaIdentity] Guid SagaId, 
    PickupId PickupId
);

public record SendTextMessagesForMatchedJobCompletedCommand(
    [property: SagaIdentity] Guid SagaId, 
    PickupId PickupId
);

public sealed class PickupMatchedEventSaga : Saga
{
    public Guid Id { get; set; }
    public PickupId PickupId { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public bool RemoveMatchedJobFromQueryListCompleted { get; set; }
    public bool SendNotificationsForMatchedJobCompleted { get; set; }

    public static async Task<(PickupMatchedEventSaga, RemoveMatchedJobFromQueryListCommand)> Start(PickupMatchedEvent @event)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var saga = new PickupMatchedEventSaga
        {
            Id = Guid.NewGuid(),
            PickupId = @event.Id,
            StartedAt = DateTimeOffset.UtcNow
        };

        var command = new RemoveMatchedJobFromQueryListCommand(new PickupId(saga.Id), saga.Id);
        return(saga, command);
    }

    public SendNotificationsForMatchedJobCommand Handle(RemoveMatchedJobFromQueryListCompletedCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        RemoveMatchedJobFromQueryListCompleted = true;
        return new SendNotificationsForMatchedJobCommand(command.SagaId, command.PickupId);
    }

    public SendTextMessagesForMatchedJobCommand Handle(SendNotificationsForMatchedJobCompletedCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        SendNotificationsForMatchedJobCompleted = true;
        return new SendTextMessagesForMatchedJobCommand(command.SagaId, command.PickupId);
    }
}