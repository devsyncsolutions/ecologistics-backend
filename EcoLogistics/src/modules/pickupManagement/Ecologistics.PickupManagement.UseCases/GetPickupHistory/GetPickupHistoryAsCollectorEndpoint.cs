// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupHistory;

public sealed class GetPickupHistoryAsCollectorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/pickup-management/pickup-jobs/history/as-collector",
            Handle)
            .WithName("GetPickupHistoryAsCollector")
            .WithDescription("Gets pickup history as a collector")
            .WithTags("Pickup Management")
            .Produces<PagedResponse<PickupHistoryAsCollectorDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(nameof(AccountType.Collector));
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string type = "",
        [FromQuery] string priority = "",
        [FromQuery] string wasteContentName = "",
        [FromQuery] string fromDate = "",
        [FromQuery] string toDate = "",
        CancellationToken cancellationToken = default)
    {
        var query = new GetPickupHistoryAsCollectorQuery(
            page, pageSize, type, priority, wasteContentName, fromDate, toDate);

        PagedResponse<PickupHistoryAsCollectorDto> result = await bus.InvokeAsync<PagedResponse<PickupHistoryAsCollectorDto>>(query, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
