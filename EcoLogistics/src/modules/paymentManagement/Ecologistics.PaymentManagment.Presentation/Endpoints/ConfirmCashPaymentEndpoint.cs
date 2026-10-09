// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagment.Presentation.Endpoints;

public sealed class ConfirmCashPaymentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/payment-management/confirm-cash-payment",
                Handle)
            .WithName("ConfirmCashPayment")
            .WithDescription("Confirms cash payment.")
            .WithTags("Payment Management")
            .Accepts<ConfirmCashPaymentCommand>("application/json")
            .Produces<ConfirmCashPaymentResponse>()
            .ProducesValidationProblem()
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(nameof(AccountType.Collector));
    }

    private static async Task<IResult> Handle(
        [FromHeader(Name = "X-Request-ID")] string requestId,
        [FromBody] ConfirmCashPaymentCommand command,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(requestId))
            return Results.BadRequest(new ProblemDetails
            {
                Detail = "Request id cannot be null or empty.",
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request"
            });

        command.RequestId = requestId;

        ConfirmCashPaymentResponse result = await bus
            .InvokeAsync<ConfirmCashPaymentResponse>(command, cancellationToken)
            .ConfigureAwait(false);
        return Results.Ok(result);
    }
}
