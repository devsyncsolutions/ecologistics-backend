// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupHistory;

public record GetPickupHistoryQuery(
    int Page, int PageSize, string Status, string Type);

public record PickupHistoryDto(
    string PickupId,
    string Reference,
    string WasteContentName,
    string Type,
    string CreatedAt,
    string ScheduledForPickupAt,
    string Status);

public sealed class GetPickupHistoryQueryValidator : AbstractValidator<GetPickupHistoryQuery>
{
    public GetPickupHistoryQueryValidator()
    {
        RuleFor(x => x.Type)
            .Must(x => x is "OneTime" or "Recurring" or "Reporting")
            .WithMessage("Invalid type specified.")
            .When(x => !string.IsNullOrWhiteSpace(x.Type));

        RuleFor(x => x.Status)
            .Must(x => x is "Scheduled" or "Cancelled" or "Completed")
            .WithMessage("Invalid status specified")
            .When(x => !string.IsNullOrWhiteSpace(x.Status));
    }
}
