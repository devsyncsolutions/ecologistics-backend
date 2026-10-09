// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Contracts.Queries;

namespace Ecologistics.PaymentManagment.Presentation.Endpoints;

public sealed class GetSingleInvoice : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/payment-management/invoices/{pickupId:guid}",
            Handle)
            .WithName("GetSingleInvoice")
            .WithDescription("Gets a single invoice by pickup id.")
            .WithTags("Payment Management")
            .Produces<SingleInvoiceDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(nameof(AccountType.Collector));
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid pickupId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        SingleInvoiceDto result = await bus.InvokeAsync<SingleInvoiceDto>(new GetInvoiceByPickupIdQuery(pickupId), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
