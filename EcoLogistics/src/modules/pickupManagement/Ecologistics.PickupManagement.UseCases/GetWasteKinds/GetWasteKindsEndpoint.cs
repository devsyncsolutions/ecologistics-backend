// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetWasteKinds;

public sealed class GetWasteKindsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/waste-kinds", Handle)
            .WithName("GetWasteKinds")
            .WithDescription("Gets a list of waste kinds.")
            .WithTags("Pickup Management")
            .Produces<PagedResponse<WasteKindDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string searchKey = "",
        CancellationToken cancellationToken = default)
    {
        var query = new GetWasteKindsQuery(page, pageSize, searchKey);

        PagedResponse<WasteKindDto> result = await bus.InvokeAsync<PagedResponse<WasteKindDto>>(query, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
