// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupJobs;

public sealed class GetPickupJobsRequestValidator : AbstractValidator<GetPickupJobsQuery>
{
    public GetPickupJobsRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page size must be greater than or equal to 1.");

        RuleFor(x => x.Type)
            .Must(x => x is "OneTime" or "Recurring" or "Reporting")
            .WithMessage("Invalid type specified.")
            .When(x => !string.IsNullOrWhiteSpace(x.Type));

        RuleFor(x => x.Priority)
            .Must(x => x is "Low" or "Medium" or "High")
            .WithMessage("Invalid priority specified.")
            .When(x => !string.IsNullOrWhiteSpace(x.Priority));

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");

        RuleFor(x => x.RadiusInMeters)
            .GreaterThan(0)
            .LessThanOrEqualTo(50_000)
            .WithMessage("Radius must be between 0 and 50,000 meters.");
    }
}
