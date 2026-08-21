namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupCompletedEvent(PickupId Id) : IDomainEvents;