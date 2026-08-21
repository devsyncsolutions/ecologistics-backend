namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupMovedToProgressEvent(PickupId Id) : IDomainEvents;