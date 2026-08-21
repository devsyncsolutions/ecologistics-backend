// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CancelPickupJob;

public sealed class CancelPickupJobEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/pickup-management/pickup-jobs/{pickupId:guid}/cancel", Handle)
            .WithName("CancelPickup")
            .WithDescription("Cancels a pickup job.")
            .WithTags("Pickup Management")
            .Accepts<CancelPickupJobRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid pickupId,
        [FromHeader(Name = "X-Request-ID")] string requestId,
        [FromBody] CancelPickupJobRequest request,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(requestId))
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Request id cannot be null or empty.",
                Detail = "Request id cannot be null or empty.",
                Status = StatusCodes.Status400BadRequest
            });

        var command = new CancelPickupJobCommand(pickupId, request.Reason){RequestId = requestId};
        await bus.InvokeAsync(command, cancellationToken).ConfigureAwait(false);

        return Results.Ok();
    }
}
