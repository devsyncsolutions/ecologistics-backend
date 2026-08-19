namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupMatchedEvent(PickupId Id) : IDomainEvents;