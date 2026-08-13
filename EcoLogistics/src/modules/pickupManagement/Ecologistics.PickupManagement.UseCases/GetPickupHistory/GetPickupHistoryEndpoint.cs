// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupHistory;

public sealed class GetPickupHistoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/pickup-jobs/history/as-generator", Handle)
            .WithName("GetPickupHistory")
            .WithDescription("Gets pickup history as a generator")
            .WithTags("Pickup Management")
            .Produces<PagedResponse<PickupHistoryDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(nameof(AccountType.Generator));
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1,
        [FromQuery] string status = "",
        [FromQuery] string type = "",
        CancellationToken cancellationToken = default)
    {
        var query = new GetPickupHistoryQuery(page, pageSize, status, type);
        var result = await bus.InvokeAsync<PagedResponse<PickupHistoryDto>>(query, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
