// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagment.Presentation.Endpoints;

public sealed class GetInvoicesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/payment-management/invoices",
            Handle)
            .WithName("GetInvoices")
            .WithDescription("Gets a list of invoices.")
            .WithTags("Payment Management")
            .Produces<PagedResponse<InvoiceDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(nameof(AccountType.Generator));
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        PagedResponse<InvoiceDto> result = await bus
            .InvokeAsync<PagedResponse<InvoiceDto>>(new GetInvoicesQuery(page, pageSize), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
