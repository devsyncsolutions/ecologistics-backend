// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.InitiateCharge;

internal sealed class InitiateChargeCommandValidator : AbstractValidator<InitiateChargeCommand>
{
    public InitiateChargeCommandValidator()
    {
        RuleFor(x => x.PickupId)
            .NotEmpty()
            .WithMessage("Pickup id is required.");

        RuleFor(x => x.NewPaymentMethod)
            .NotNull()
            .When(x => string.IsNullOrWhiteSpace(x.PaymentMethodId))
            .WithMessage("New payment method is required when no payment method id is provided.");

        RuleFor(x => x.PaymentMethodId)
            .NotEmpty()
            .When(x => x.NewPaymentMethod is null)
            .WithMessage("Payment method id is required when no new payment method is provided.");

        RuleFor(x => x.NewPaymentMethod!)
            .SetValidator(new NewPaymentMethodDtoValidation())
            .When(x => x.NewPaymentMethod is not null);
    }
}

internal sealed class NewPaymentMethodDtoValidation : AbstractValidator<NewPaymentMethodDto>
{
    public NewPaymentMethodDtoValidation()
    {
        RuleFor(x => x.MethodType)
            .Must(x => x is "MtnMobileMoney" or "AirtelTigoCash" or "VodafoneCash")
            .WithMessage("Invalid payment method type specified.");

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .MaximumLength(10)
            .MinimumLength(10)
            .WithMessage("Mobile number must be 10 digits long.");
    }
}
