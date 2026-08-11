// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupJobs;

public sealed class GetPickupJobsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/pickup-jobs", Handle)
            .WithName("GetPickupJobs")
            .WithDescription("Gets a list of pickup jobs within a specified radius.")
            .WithTags("Pickup Management")
            .Produces<PagedResponse<PickupJobDto>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string type = "",
        [FromQuery] string priority = "",
        [FromQuery] double latitude = 0,
        [FromQuery] double longitude = 0,
        [FromQuery] double radiusInMeters = 50,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPickupJobsQuery(page, pageSize, type, priority, latitude, longitude, radiusInMeters);
        var result = await bus.InvokeAsync<PagedResponse<PickupJobDto>>(query, cancellationToken).ConfigureAwait(false);
        return Results.Ok(result);
    }
}
