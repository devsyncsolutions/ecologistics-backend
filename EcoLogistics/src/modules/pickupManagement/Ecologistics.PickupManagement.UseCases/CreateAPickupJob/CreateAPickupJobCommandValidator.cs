// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

internal sealed class CreateAPickupJobCommandValidator : AbstractValidator<CreateAPickupJobCommand>
{
    public CreateAPickupJobCommandValidator()
    {
        RuleFor(x => x.PickupFeeId)
            .NotEmpty()
            .WithMessage("Pickup fee id is required. It presents the selected weight and volume.");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type is required.")
            .Must(x => x is "OneTime" or "Recurring" or "Reporting")
            .WithMessage("Invalid type specified.");

        RuleFor(x => x.Priority)
            .NotEmpty()
            .WithMessage("Priority is required.")
            .Must(x => x is "Low" or "Medium" or "High")
            .WithMessage("Invalid priority specified.");

        RuleFor(x => x.FormattedAddress)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MaximumLength(300)
            .WithMessage("Address cannot exceed 300 characters.");

        RuleFor(x => x.GhanaPostGpsAddress)
            .MaximumLength(13)
            .WithMessage("GPS address cannot exceed 13 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.GhanaPostGpsAddress));

        RuleFor(x => x.Landmark)
            .MaximumLength(255)
            .WithMessage("Landmark cannot exceed 255 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Landmark));

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

        RuleFor(x => x.Coordinates)
            .NotEmpty()
            .WithMessage("Coordinates are required.");

        RuleFor(x => x.Coordinates.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Coordinates.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");

        RuleFor(x => x.DaysIntervalForPickupIfRecurring)
            .GreaterThan(1)
            .When(x => x.Type.Equals("Recurring", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Recurring interval must be greater than or equal to 1.");
    }

    private static bool BeAValidDateAndTime(string? arg)
    {
        return !string.IsNullOrWhiteSpace(arg) &&
               DateTime.TryParseExact(arg, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
