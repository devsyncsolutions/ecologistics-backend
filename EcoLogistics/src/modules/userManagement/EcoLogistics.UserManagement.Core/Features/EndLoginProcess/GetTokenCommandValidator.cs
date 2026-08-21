// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.EndLoginProcess;

public sealed class GetTokenCommandValidator : AbstractValidator<GetTokenCommand>
{
    public GetTokenCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.OtpCode)
            .NotEmpty().WithMessage("OTP code is required.")
            .Length(6).WithMessage("OTP code must be 6 digits long.");

        RuleFor(x => x.Platform)
            .Must(platform => platform is "Android" or "IOS")
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceTokenForNotificationBroadcasting))
            .WithMessage("Platform must be valid");

        RuleFor(x => x.DeviceTokenForNotificationBroadcasting)
            .NotEmpty()
            .When(x => !string.IsNullOrWhiteSpace(x.Platform))
            .WithMessage("Token is required when platform is set");
    }
}
