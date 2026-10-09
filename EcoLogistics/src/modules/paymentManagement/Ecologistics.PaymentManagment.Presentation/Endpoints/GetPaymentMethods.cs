// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagment.Presentation.Endpoints;

public sealed class GetPaymentMethodsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/payment-management/payment-methods",
            Handle)
            .WithName("GetPaymentMethods")
            .WithDescription("Gets a list of payment methods.")
            .WithTags("Payment Management")
            .Produces<IEnumerable<UserPaymentMethodDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> Handle(IMessageBus bus, CancellationToken cancellationToken)
    {
        IEnumerable<UserPaymentMethodDto> result = await bus
            .InvokeAsync<IEnumerable<UserPaymentMethodDto>>(new GetPaymentMethodsQuery(), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
