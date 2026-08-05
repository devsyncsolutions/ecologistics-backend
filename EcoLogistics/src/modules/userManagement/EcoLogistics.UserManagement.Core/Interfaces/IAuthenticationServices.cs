// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Interfaces;

public interface IAuthenticationServices
{
    Task<string>GenerateOtpCodeAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<(string, string)>AuthenticateAsync(string phoneNumber, string otpCode, CancellationToken cancellationToken = default);
    Task<(string, string)>RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<string> RegisterAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}
