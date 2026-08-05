// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;

namespace EcoLogistics.UserManagement.Core.Features.UpdateUserDetails;

public sealed class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
{
    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(15).WithMessage("First name cannot exceed 15 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(15).WithMessage("Last name cannot exceed 15 characters.");

        RuleFor(x => x.OtherNames)
            .MaximumLength(50).WithMessage("Other names cannot exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.OtherNames));

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(gender => gender is "Male" or "Female").WithMessage("Invalid gender specified.");

        RuleFor(x => x.Residence)
            .MaximumLength(100).WithMessage("Residence cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Residence));

        RuleFor(x => x.ResidenceGps)
            .MaximumLength(13).WithMessage("Residence GPS cannot exceed 13 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.ResidenceGps));

        RuleFor(x => x.DateOfBirth)
            .Must(BeAValidDate).WithMessage("Date of birth must be a valid date in the format yyyy-MM-dd.")
            .When(x => !string.IsNullOrWhiteSpace(x.DateOfBirth));
    }

    private static bool BeAValidDate(string arg)
    {
        return DateTime.TryParseExact(arg, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
