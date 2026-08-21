namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupMatchedEvent(PickupId Id, string PickupReference, string TriggeredBy, string UserId) : IDomainEvents;
