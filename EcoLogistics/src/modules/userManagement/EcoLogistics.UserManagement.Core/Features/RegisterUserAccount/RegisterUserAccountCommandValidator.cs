// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;

public sealed class RegisterUserAccountCommandValidator : AbstractValidator<RegisterUserAccountCommand>
{
    public RegisterUserAccountCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(15).WithMessage("First name cannot exceed 15 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(15).WithMessage("Last name cannot exceed 15 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.OtherNames)
            .MaximumLength(50).WithMessage("Other names cannot exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.OtherNames));

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(gender => gender is "Male" or "Female").WithMessage("Invalid gender specified.");
    }
}
