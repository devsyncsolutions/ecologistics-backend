// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupHistory;

public record GetPickupHistoryAsCollectorQuery(
    int Page,
    int PageSize,
    string? Type,
    string? Priority,
    string? WasteContentName,
    string? FromDate,
    string? ToDate);

internal sealed class GetPickupHistoryAsCollectorQueryValidator : AbstractValidator<GetPickupHistoryAsCollectorQuery>
{
    public GetPickupHistoryAsCollectorQueryValidator()
    {
        RuleFor(x => x.Type)
            .Must(x => x is "OneTime" or "Recurring" or "Reporting")
            .WithMessage("Invalid type specified.")
            .When(x => !string.IsNullOrWhiteSpace(x.Type));

        RuleFor(x => x.Priority)
            .Must(x => x is "Low" or "Medium" or "High")
            .WithMessage("Invalid priority specified.")
            .When(x => !string.IsNullOrWhiteSpace(x.Priority));
    }
}
