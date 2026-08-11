// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupFees;

public sealed class GetPickupFeesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/measurements", Handle)
            .WithName("GetPickupFees")
            .WithDescription("Gets a list of measurements also as pickup fees.")
            .WithTags("Pickup Management")
            .Produces<IReadOnlyList<GetPickupFeesResponse>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        var result = await bus
            .InvokeAsync<IReadOnlyList<GetPickupFeesResponse>>(new GetPickupFeesQuery(), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
