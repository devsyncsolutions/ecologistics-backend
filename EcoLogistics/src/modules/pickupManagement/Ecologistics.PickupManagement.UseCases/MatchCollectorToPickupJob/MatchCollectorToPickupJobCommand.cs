namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public record MatchCollectorToPickupJobCommand(Guid PickupJobId) : BaseRequest;