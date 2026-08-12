// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public sealed class CreatAPickupJobEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/pickup-management/pickup-jobs", Handle)
            .WithName("CreatAPickupJob")
            .WithDescription("Creates a new pickup job.")
            .WithTags("Pickup Management")
            .Accepts<CreateAPickupJobCommand>("application/json")
            .Produces<CreateAPickupJobResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromHeader(Name = "X-Request-ID")] string requestId,
        [FromBody] CreateAPickupJobCommand command,
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

        command.RequestId = requestId;

        var result = await bus.InvokeAsync<CreateAPickupJobResponse>(command, cancellationToken).ConfigureAwait(false);

        return Results.Created(
            "/pickup-management/pickup-jobs/" + result.PickupId,
            result);
    }
}
