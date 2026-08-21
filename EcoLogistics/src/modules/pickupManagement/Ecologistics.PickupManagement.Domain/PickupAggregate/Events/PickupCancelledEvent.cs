namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupCancelledEvent(PickupId Id) : IDomainEvents;