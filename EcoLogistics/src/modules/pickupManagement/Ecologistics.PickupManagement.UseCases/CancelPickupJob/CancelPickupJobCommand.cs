// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CancelPickupJob;

public record CancelPickupJobCommand(Guid PickupId, string Reason) : BaseRequest;

public record CancelPickupJobRequest(string Reason);

public class CancelPickupJobCommandValidator : AbstractValidator<CancelPickupJobCommand>
{
    public CancelPickupJobCommandValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage("Reason is required.")
            .MaximumLength(500)
            .WithMessage("Reason cannot exceed 500 characters.");
    }
}
