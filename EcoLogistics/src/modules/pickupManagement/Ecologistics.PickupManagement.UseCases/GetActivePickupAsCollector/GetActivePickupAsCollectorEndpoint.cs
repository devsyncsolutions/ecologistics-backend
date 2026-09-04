// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetActivePickupAsCollector;

public sealed class GetActivePickupAsCollectorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/pickup-jobs/active-job/as-collector", Handle)
            .WithName("GetActivePickupJobAsCollector")
            .WithDescription("Gets the active pickup job as a collector")
            .WithTags("Pickup Management")
            .Produces<ActivePickupDto?>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(nameof(AccountType.Collector));
    }

    private static async Task<IResult> Handle([FromServices] IMessageBus bus, CancellationToken cancellationToken)
    {
        ActivePickupDto? result = await bus
            .InvokeAsync<ActivePickupDto?>(new GetActivePickupAsCollectorQuery(), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
