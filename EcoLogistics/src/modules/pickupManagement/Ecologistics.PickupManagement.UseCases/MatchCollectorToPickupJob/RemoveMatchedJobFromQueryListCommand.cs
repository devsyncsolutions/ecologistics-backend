namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public record RemoveMatchedJobFromQueryListCommand(
    PickupId PickupId,
    string PickupReference,
    Guid SagaId,
    string TriggeredBy,
    string UserId);
