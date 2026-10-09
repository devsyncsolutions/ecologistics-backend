// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagment.Presentation.Endpoints;

public sealed class GetPaymentDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/payment-management/payments/{paymentId:guid}",
            Handle)
            .WithName("GetPaymentDetails")
            .WithDescription("Gets payment details.")
            .WithTags("Payment Management")
            .Produces<PaymentDetailsDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> Handle(
        [FromRoute] Guid paymentId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(bus);

        PaymentDetailsDto result = await bus.InvokeAsync<PaymentDetailsDto>(
            new GetPaymentDetailsQuery(paymentId),
            cancellationToken
        ).ConfigureAwait(false);

        return TypedResults.Ok(result);
    }
}
