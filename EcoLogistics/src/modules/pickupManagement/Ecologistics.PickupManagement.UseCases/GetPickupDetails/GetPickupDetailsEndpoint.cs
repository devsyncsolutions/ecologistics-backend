// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupDetails;

public sealed class GetPickupDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/pickup-jobs/{pickupId:guid}", Handle)
            .WithName("GetPickupJobDetails")
            .WithDescription("Gets a pickup job details.")
            .WithTags("Pickup Management")
            .Produces<PickupDetailsResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid pickupId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        PickupDetailsResponse result = await bus
            .InvokeAsync<PickupDetailsResponse>(new GetPickupDetailsQuery(pickupId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
