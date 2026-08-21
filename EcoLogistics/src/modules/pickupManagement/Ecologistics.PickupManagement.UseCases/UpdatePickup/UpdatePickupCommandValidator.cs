// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.UpdatePickup;

public sealed class UpdatePickupCommandValidator : AbstractValidator<UpdatePickupCommand>
{
    public UpdatePickupCommandValidator()
    {
        RuleFor(x => x.Priority)
            .NotEmpty()
            .WithMessage("Priority is required.")
            .Must(x => x is "Low" or "Medium" or "High")
            .WithMessage("Invalid priority specified.");

        RuleFor(x => x.ContactName)
            .MaximumLength(50)
            .WithMessage("Contact name cannot exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.ContactName));

        RuleFor(x => x.ContactPhone)
            .MaximumLength(15)
            .WithMessage("Contact phone cannot exceed 15 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.ContactPhone));

        RuleFor(x => x.ContactPhone)
            .NotEmpty()
            .WithMessage("Contact phone is required when an alternative Contact Name is provided")
            .When(x => !string.IsNullOrWhiteSpace(x.ContactName));

        RuleFor(x => x.ContactName)
            .NotEmpty()
            .WithMessage("Contact Name is required when an alternative Contact phone is provided")
            .When(x => !string.IsNullOrWhiteSpace(x.ContactPhone));

        RuleFor(x => x.ExtraNotes)
            .MaximumLength(500)
            .WithMessage("Extra notes cannot exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.ExtraNotes));

        RuleFor(x => x.ScheduledForPickupAt)
            .NotEmpty()
            .WithMessage("Scheduled pickup date is required.")
            .Must(BeAValidDateAndTime).WithMessage("Scheduled pickup date must be a valid date and time.");

        RuleFor(x => x.WasteContentName)
            .NotEmpty()
            .WithMessage("Waste content name is required.")
            .MaximumLength(100)
            .WithMessage("Waste content name cannot exceed 100 characters.");
    }

    private static bool BeAValidDateAndTime(string? arg)
    {
        return !string.IsNullOrWhiteSpace(arg) &&
               DateTime.TryParseExact(arg, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
