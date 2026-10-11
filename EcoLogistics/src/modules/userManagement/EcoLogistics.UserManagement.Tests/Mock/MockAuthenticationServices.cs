// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcoLogistics.UserManagement.Core.Enums;
using EcoLogistics.UserManagement.Core.Interfaces;
using EcoLogistics.UserManagement.Core.Models;

namespace EcoLogistics.UserManagement.Tests.Mock;

/// <summary>
/// Mock implementation of IAuthenticationServices for testing purposes.
/// </summary>
internal sealed class MockAuthenticationServices : IAuthenticationServices
{
    private readonly Dictionary<string, ApplicationUser> _registeredUsers = new();
    public int RegisterAsyncCallCount { get; private set; }
    public ApplicationUser? LastRegisteredUser { get; private set; }

    public string OtpCodeToReturn { get; set; } = "123456";
    public int GenerateOtpCodeAsyncCallCount { get; private set; }
    public string? LastPhoneNumberForOtp { get; private set; }

    public Task<string> GenerateOtpCodeAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        GenerateOtpCodeAsyncCallCount++;
        LastPhoneNumberForOtp = phoneNumber;
        return Task.FromResult(OtpCodeToReturn);
    }

    public Task<(string, string)> AuthenticateAsync(string phoneNumber, string otpCode, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<(string, string)> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> RegisterAsync(ApplicationUser user, AccountType accountType, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        var userId = Guid.NewGuid().ToString();
        _registeredUsers[userId] = user;
        RegisterAsyncCallCount++;
        LastRegisteredUser = user;

        return Task.FromResult(userId);
    }

    public Task<IReadOnlyCollection<AccountType>> GetUserRolesAsync(string username, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    /// <summary>
    /// Gets a registered user by ID for verification purposes.
    /// </summary>
    public ApplicationUser? GetRegisteredUser(string userId)
    {
        return _registeredUsers.TryGetValue(userId, out var user) ? user : null;
    }

    /// <summary>
    /// Gets all registered users for verification purposes.
    /// </summary>
    public IEnumerable<ApplicationUser> GetAllRegisteredUsers() => _registeredUsers.Values;

    /// <summary>
    /// Resets the mock for a clean test state.
    /// </summary>
    public void Reset()
    {
        _registeredUsers.Clear();
        RegisterAsyncCallCount = 0;
        LastRegisteredUser = null;
        GenerateOtpCodeAsyncCallCount = 0;
        LastPhoneNumberForOtp = null;
        OtpCodeToReturn = "123456";
    }
}
