// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetAllDistricts;

public sealed class GetAllDistrictsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/districts", Handle)
            .WithName("GetDistricts")
            .WithDescription("Gets a list of all districts.")
            .WithTags("Pickup Management")
            .Produces<PagedResponse<GetAllDistrictDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .AllowAnonymous();
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        [FromQuery] string region = "",
        [FromQuery] string searchKey = "",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllDistrictsQuery(region, searchKey, page, pageSize);
        var result = await bus.InvokeAsync<PagedResponse<GetAllDistrictDto>>(query, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
