// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ecologistics.Common.Enums;
using Ecologistics.Common.Exceptions;
using EcoLogistics.UserManagement.Core.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EcoLogistics.UserManagement.Infrastructure.Services;

public sealed class AuthenticationServices(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    JwtSettings jwtSettings)
    : IAuthenticationServices
{
    public async Task<string> GenerateOtpCodeAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByNameAsync(phoneNumber).ConfigureAwait(false);

        if (user is null)
            throw new NotFoundException($"User with phone number {phoneNumber} not found.");

        string otpCode = await userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider).ConfigureAwait(false);

        // reset password with the generated OTP code
        string token = await userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
        var resetResult = await userManager.ResetPasswordAsync(user, token, otpCode).ConfigureAwait(false);

        if (!resetResult.Succeeded) return otpCode;

        user.OtpCodeExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30).TimeOfDay;
        _ = await userManager.UpdateAsync(user).ConfigureAwait(false);

        return otpCode;
    }

    public async Task<(string, string)> AuthenticateAsync(string phoneNumber, string otpCode,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user =
            await userManager.FindByNameAsync(phoneNumber).ConfigureAwait(false) ??
            throw new NotFoundException($"User with phone number {phoneNumber} not found.");

        // check if the OTP code has expired
        if (user.OtpCodeExpiresAt < DateTimeOffset.UtcNow.TimeOfDay)
            throw new BadHttpRequestException("OTP code has expired.");

        SignInResult result = await signInManager.PasswordSignInAsync(user.UserName!, otpCode, false, lockoutOnFailure: false).ConfigureAwait(false);

        if (!result.Succeeded)
            throw new BadHttpRequestException("Invalid OTP code.");

        string accessToken = await GenerateAccessToken(user).ConfigureAwait(false);
        string refreshToken = GenerateRefreshToken();

        if (user.PhoneNumberConfirmed) return (accessToken, refreshToken);

        user.PhoneNumberConfirmed = true;
        await userManager.UpdateAsync(user).ConfigureAwait(false);

        return (accessToken, refreshToken);
    }

    private static string GenerateRefreshToken()
    {
        // Implement refresh token generation
        return string.Empty;
    }

    private async ValueTask<string> GenerateAccessToken(ApplicationUser user)
    {
        // validate token expiration configuration
        if (jwtSettings.AccessTokenExpirationMinutes <= 0 || jwtSettings.AccessTokenExpirationMinutes > 1440)
            throw new InvalidOperationException("AccessTokenExpirationMinutes must be between 1 and 1440.");

        var userClaims = await userManager.GetClaimsAsync(user).ConfigureAwait(false);
        var roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);

        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("phone", user.PhoneNumber!),
            new Claim("districtId", user.DistrictId.ToString())
        }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public Task<(string, string)> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public async Task<string> RegisterAsync(ApplicationUser user, AccountType accountType, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        var existingUser = await userManager.FindByNameAsync(user.UserName!).ConfigureAwait(false);
        if (existingUser != null)
            throw new BadHttpRequestException("User already exists.", StatusCodes.Status400BadRequest);

        var result = await userManager.CreateAsync(user).ConfigureAwait(false);

        if (!result.Succeeded)
            throw new BadHttpRequestException("Failed to register user.", StatusCodes.Status400BadRequest);

        await userManager.AddToRoleAsync(user, accountType.ToString()).ConfigureAwait(false);

        return user.Id;
    }

    public async Task<IReadOnlyCollection<AccountType>> GetUserRolesAsync(string username, CancellationToken cancellationToken = default)
    {
        ApplicationUser user = await userManager.FindByNameAsync(username).ConfigureAwait(false) ??
            throw new NotFoundException($"User with username {username} not found.");

        IList<string> roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
        return roles.Select(Enum.Parse<AccountType>).ToList();
    }
}
