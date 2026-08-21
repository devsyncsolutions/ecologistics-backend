// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.StartAScheduledJob;

public sealed class StartPickupEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/pickup-management/pickup-jobs/{pickupId:guid}/start", Handle)
            .WithName("StartPickup")
            .WithDescription("Starts a pickup job.")
            .WithTags("Pickup Management")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(nameof(AccountType.Collector));
    }

    private static async Task<IResult> Handle(
        [FromHeader(Name = "X-Request-ID")] string requestId,
        [FromRoute] Guid pickupId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(requestId))
            return Results.BadRequest(new ProblemDetails
            {
                Detail = "Request id cannot be null or empty.",
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request"
            });

        var command = new StartAScheduledJobCommand(pickupId);
        await bus.InvokeAsync(command, cancellationToken).ConfigureAwait(false);

        return Results.Ok();
    }
}
