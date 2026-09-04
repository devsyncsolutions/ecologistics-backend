namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupCompletedEvent(
    PickupId Id,
    string CollectorMemberId,
    string CreatedBy,
    string PickupReference) : IDomainEvents;
