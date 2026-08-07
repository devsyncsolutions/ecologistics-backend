// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcoLogistics.UserManagement.Core.Enums;
using EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;
using EcoLogistics.UserManagement.Tests.Mock;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Runtime;
using ZiggyCreatures.Caching.Fusion;

namespace EcoLogistics.UserManagement.Tests.UnitTests.RegisterUserAccount;

public sealed class RegisterUserAccountTests : IDisposable
{
    private readonly FusionCache _cache;
    private MockAuthenticationServices _mockAuthenticationServices = null!;
    private RegisterUserAccountCommandHandler _handler = null!;

    public RegisterUserAccountTests()
    {
        _cache = new FusionCache(new FusionCacheOptions());
        SetupMocks();
    }

    private void SetupMocks()
    {
        _mockAuthenticationServices = new MockAuthenticationServices();
        _handler = new RegisterUserAccountCommandHandler(_mockAuthenticationServices, _cache);
    }

    #region Happy Path Tests

    [Fact]
    public async Task HandleWithValidCommandShouldRegisterUserSuccessfully()
    {
        // Arrange
        var command = RegisterUserAccountCommandBuilder.CreateValid();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(string.IsNullOrEmpty(response.UserId));
        Assert.Equal(1, _mockAuthenticationServices.RegisterAsyncCallCount);
    }

    [Fact]
    public async Task HandleWithValidCommandShouldMapCommandToApplicationUserCorrectly()
    {
        // Arrange
        const string firstName = "Jane";
        const string lastName = "Smith";
        const string otherNames = "Mary";
        const string gender = "Female";
        const string phoneNumber = "0541234567";

        var command = new RegisterUserAccountCommandBuilder()
            .WithFirstName(firstName)
            .WithLastName(lastName)
            .WithOtherNames(otherNames)
            .WithGender(gender)
            .WithPhoneNumber(phoneNumber)
            .Build();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var registeredUser = _mockAuthenticationServices.LastRegisteredUser;
        Assert.NotNull(registeredUser);
        Assert.Equal(firstName, registeredUser.FirstName);
        Assert.Equal(lastName, registeredUser.LastName);
        Assert.Equal(otherNames, registeredUser.OtherNames);
        Assert.Equal(phoneNumber, registeredUser.PhoneNumber);
        Assert.Equal(phoneNumber, registeredUser.UserName);
    }

    [Fact]
    public async Task HandleWithNullOtherNamesShouldRegisterUserSuccessfully()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithOtherNames(null)
            .Build();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(string.IsNullOrEmpty(response.UserId));
        var registeredUser = _mockAuthenticationServices.LastRegisteredUser;
        Assert.Null(registeredUser?.OtherNames);
    }

    [Theory]
    [InlineData("Male")]
    [InlineData("Female")]
    public async Task HandleWithValidGenderShouldParseGenderCorrectly(string gender)
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithGender(gender)
            .Build();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        var registeredUser = _mockAuthenticationServices.LastRegisteredUser;
        Assert.NotNull(registeredUser);
        Assert.Equal(Enum.Parse<Gender>(gender), registeredUser.Gender);
    }

    #endregion

    #region Validation Tests

    [Fact]
    public async Task HandleWithNullCommandShouldThrowBadHttpRequestException()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(
            () => _handler.Handle(null!, CancellationToken.None));

        Assert.Equal("Command cannot be null", exception.Message);
    }

    [Fact]
    public async Task HandleWithEmptyFirstNameShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithFirstName(string.Empty)
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "FirstName");
    }

    [Fact]
    public async Task HandleWithEmptyLastNameShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithLastName(string.Empty)
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "LastName");
    }

    [Fact]
    public async Task HandleWithEmptyPhoneNumberShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithPhoneNumber(string.Empty)
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "PhoneNumber");
    }

    [Fact]
    public async Task HandleWithEmptyGenderShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithGender(string.Empty)
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "Gender");
    }

    [Fact]
    public async Task HandleWithInvalidGenderShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithGender("Invalid")
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "Gender");
    }

    [Fact]
    public async Task HandleWithFirstNameExceeding15CharactersShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithFirstName("VeryLongFirstName")
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "FirstName");
    }

    [Fact]
    public async Task HandleWithLastNameExceeding15CharactersShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithLastName("VeryLongLastName")
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "LastName");
    }

    [Fact]
    public async Task HandleWithPhoneNumberExceeding15CharactersShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithPhoneNumber("12345678901234567890")
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "PhoneNumber");
    }

    [Fact]
    public async Task HandleWithOtherNamesExceeding50CharactersShouldThrowValidationException()
    {
        // Arrange
        var longOtherNames = new string('A', 51);
        var command = new RegisterUserAccountCommandBuilder()
            .WithOtherNames(longOtherNames)
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "OtherNames");
    }

    #endregion

    #region Caching Tests

    [Fact]
    public async Task HandleWithSameRequestIdShouldReturnCachedResultOnSecondCall()
    {
        // Arrange
        var requestId = Guid.NewGuid().ToString();
        var command1 = new RegisterUserAccountCommandBuilder()
            .WithRequestId(requestId)
            .Build();

        // Act
        var response1 = await _handler.Handle(command1, CancellationToken.None);

        // Modify mock for second call to verify cache is used
        _mockAuthenticationServices.Reset();

        var command2 = new RegisterUserAccountCommandBuilder()
            .WithRequestId(requestId)
            .Build();
        var response2 = await _handler.Handle(command2, CancellationToken.None);

        // Assert
        Assert.Equal(response1.UserId, response2.UserId);
        // RegisterAsync should not be called again after cache hit (count was reset to 0 before second call)
        Assert.Equal(0, _mockAuthenticationServices.RegisterAsyncCallCount);
    }

    [Fact]
    public async Task HandleWithDifferentRequestIdsShouldNotUseCache()
    {
        // Arrange
        var command1 = new RegisterUserAccountCommandBuilder()
            .WithRequestId(Guid.NewGuid().ToString())
            .Build();

        var command2 = new RegisterUserAccountCommandBuilder()
            .WithRequestId(Guid.NewGuid().ToString())
            .Build();

        // Act
        var response1 = await _handler.Handle(command1, CancellationToken.None);
        var response2 = await _handler.Handle(command2, CancellationToken.None);

        // Assert
        Assert.NotEqual(response1.UserId, response2.UserId);
        Assert.Equal(2, _mockAuthenticationServices.RegisterAsyncCallCount);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public async Task HandleWithWhitespaceFirstNameShouldThrowValidationException()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithFirstName("   ")
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
    }

    [Fact]
    public async Task HandleWithMaximumAllowedLengthFirstNameShouldSucceed()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithFirstName("JohnDoeTheLonge")  // exactly 15 characters
            .Build();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(string.IsNullOrEmpty(response.UserId));
    }

    [Fact]
    public async Task HandleWithEmptyOtherNamesShouldRegisterUserSuccessfully()
    {
        // Arrange
        var command = new RegisterUserAccountCommandBuilder()
            .WithOtherNames(string.Empty)
            .Build();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(string.IsNullOrEmpty(response.UserId));
    }

    [Fact]
    public async Task HandleWithWhitespaceOtherNamesShouldRegisterUserSuccessfully()
    {
        // Arrange - whitespace-only otherNames should be treated as empty by the validator
        var command = new RegisterUserAccountCommandBuilder()
            .WithOtherNames("   ")
            .Build();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(string.IsNullOrEmpty(response.UserId));
    }

    #endregion

    #region Cancellation Token Tests

    [Fact]
    public async Task HandleWithCancellationTokenShouldRespectCancellation()
    {
        // Arrange
        var command = RegisterUserAccountCommandBuilder.CreateValid();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync().ConfigureAwait(true);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            () => _handler.Handle(command, cts.Token));
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion
}
