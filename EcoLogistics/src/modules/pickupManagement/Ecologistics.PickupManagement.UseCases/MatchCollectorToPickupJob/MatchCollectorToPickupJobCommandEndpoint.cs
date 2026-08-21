namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public sealed class MatchCollectorToPickupJobCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/pickup-management/pickup-jobs/{pickupJobId:guid}/accept", Handle)
            .WithName("AcceptAPickupJob")
            .WithDescription("Accept a new pickup job.")
            .WithTags("Pickup Management")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(nameof(AccountType.Collector));
    }

    private static async Task<IResult> Handle(
        [FromHeader(Name = "X-Request-ID")] string requestId,
        [FromRoute] Guid pickupJobId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken
    )
    {
        if (string.IsNullOrWhiteSpace(requestId))
            return Results.BadRequest(new ProblemDetails
            {
                Detail = "Request id cannot be null or empty.",
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request"
            });

        var command = new MatchCollectorToPickupJobCommand(pickupJobId) { RequestId = requestId};
        await bus
            .InvokeAsync(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok();
    }
}
