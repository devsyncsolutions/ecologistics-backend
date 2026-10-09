// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.ConfirmCashPayment;

public record ConfirmCashPaymentCommand(
    string PickupId) : BaseRequest;

public record ConfirmCashPaymentResponse(
    string PaymentId,
    string Status);

public class ConfirmCashPaymentCommandValidator : AbstractValidator<ConfirmCashPaymentCommand>
{
    public ConfirmCashPaymentCommandValidator()
    {
        RuleFor(x => x.PickupId)
            .NotEmpty()
            .WithMessage("Pickup id is required.");
    }
}
