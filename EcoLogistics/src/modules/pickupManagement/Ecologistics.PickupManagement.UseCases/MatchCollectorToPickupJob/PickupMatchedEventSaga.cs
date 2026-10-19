namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public record RemoveMatchedJobFromQueryListCompletedCommand(
    [property: SagaIdentity] Guid SagaId,
    PickupId PickupId,
    string PickupReference,
    string TriggeredBy,
    string UserId);

public record SendNotificationsForMatchedJobCommand(
    [property: SagaIdentity] Guid SagaId,
    PickupId Id,
    string PickupReference,
    string TriggeredBy,
    string UserId);

public record SendNotificationsForMatchedJobCompletedCommand(
    [property: SagaIdentity] Guid SagaId,
    PickupId PickupId,
    string PickupReference,
    string TriggeredBy,
    string UserId
);

public record SendTextMessagesForMatchedJobCommand(
    [property: SagaIdentity] Guid SagaId,
    PickupId PickupId,
    string PickupReference,
    string TriggeredBy,
    string UserId
);

public record SendTextMessagesForMatchedJobCompletedCommand(
    [property: SagaIdentity] Guid SagaId,
    PickupId PickupId
);

public record GeneratePickupJobInvoiceCompletedCommand(
    [property: SagaIdentity] Guid SagaId);

public sealed class PickupMatchedEventSaga : Saga
{
    public Guid Id { get; set; }
    public PickupId PickupId { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public bool RemoveMatchedJobFromQueryListCompleted { get; set; }
    public bool SendNotificationsForMatchedJobCompleted { get; set; }
    public bool SendTextMessagesForMatchedJobCompleted { get; set; }
    public bool GeneratePickupJobInvoiceCompleted { get; set; }

    public static async Task<(PickupMatchedEventSaga, RemoveMatchedJobFromQueryListCommand)> Start(
        PickupMatchedEvent @event,
        ITimeTickerManager<TimeTickerEntity> timeTicker,
        TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(@event);
        ArgumentNullException.ThrowIfNull(timeTicker);
        ArgumentNullException.ThrowIfNull(timeProvider);

        var saga = new PickupMatchedEventSaga
        {
            Id = Guid.NewGuid(),
            PickupId = @event.Id,
            StartedAt = timeProvider.GetUtcNow()
        };

        var command = new RemoveMatchedJobFromQueryListCommand(
            saga.PickupId,
            @event.PickupReference,
            saga.Id,
            @event.TriggeredBy,
            @event.UserId);
        return(saga, command);
    }

    public SendNotificationsForMatchedJobCommand Handle(RemoveMatchedJobFromQueryListCompletedCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        RemoveMatchedJobFromQueryListCompleted = true;
        return new SendNotificationsForMatchedJobCommand(
            command.SagaId,
            command.PickupId,
            command.PickupReference,
            command.TriggeredBy,
            command.UserId);
    }

    public SendTextMessagesForMatchedJobCommand Handle(SendNotificationsForMatchedJobCompletedCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        SendNotificationsForMatchedJobCompleted = true;
        return new SendTextMessagesForMatchedJobCommand(
            command.SagaId,
            command.PickupId,
            command.PickupReference,
            command.TriggeredBy,
            command.UserId);
    }

    public GeneratePickupJobInvoiceRequest Handle(SendTextMessagesForMatchedJobCompletedCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        SendTextMessagesForMatchedJobCompleted = true;
        return new GeneratePickupJobInvoiceRequest(command.PickupId.Value, command.SagaId);
    }

    public void Handle(GeneratePickupJobInvoiceCompletedCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        GeneratePickupJobInvoiceCompleted = true;
        MarkCompleted();
    }
}
