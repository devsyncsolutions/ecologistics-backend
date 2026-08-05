// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcoLogistics.UserManagement.Core.Features.StartLoginProcess;
using EcoLogistics.UserManagement.Tests.Mock;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ZiggyCreatures.Caching.Fusion;

namespace EcoLogistics.UserManagement.Tests.UnitTests.StartLoginProcess;

public sealed class StartLoginProcessTests : IDisposable
{
    private readonly FusionCache _cache;
    private MockAuthenticationServices _mockAuthenticationServices = null!;
    private MockMessageBus _mockMessageBus = null!;
    private LoginCommandHandler _handler = null!;

    public StartLoginProcessTests()
    {
        _cache = new FusionCache(new FusionCacheOptions());
        SetupMocks();
    }

    private void SetupMocks()
    {
        _mockAuthenticationServices = new MockAuthenticationServices();
        _mockMessageBus = new MockMessageBus();
        _handler = new LoginCommandHandler(_mockAuthenticationServices, _mockMessageBus, _cache);
    }

    #region Happy Path Tests

    [Fact]
    public async Task HandleWithValidCommandShouldReturnResponseWithPhoneNumber()
    {
        // Arrange
        var command = LoginCommandBuilder.CreateValid();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(command.PhoneNumber, response.PhoneNumber);
    }

    [Fact]
    public async Task HandleWithValidCommandShouldGenerateOtpCode()
    {
        // Arrange
        var command = LoginCommandBuilder.CreateValid();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(1, _mockAuthenticationServices.GenerateOtpCodeAsyncCallCount);
        Assert.Equal(command.PhoneNumber, _mockAuthenticationServices.LastPhoneNumberForOtp);
    }

    [Fact]
    public async Task HandleWithValidCommandShouldPublishSendOtpCodeCommandToMessageBus()
    {
        // Arrange
        var command = LoginCommandBuilder.CreateValid();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(1, _mockMessageBus.PublishAsyncCallCount);
        var published = Assert.IsType<SendOtpCodeCommand>(_mockMessageBus.LastPublishedMessage);
        Assert.Equal(command.PhoneNumber, published.PhoneNumber);
        Assert.Equal(_mockAuthenticationServices.OtpCodeToReturn, published.OtpCode);
    }

    [Fact]
    public async Task HandleWithSpecificPhoneNumberShouldPublishCorrectPhoneNumber()
    {
        // Arrange
        const string phoneNumber = "+233501234567";
        var command = new LoginCommandBuilder()
            .WithPhoneNumber(phoneNumber)
            .Build();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var published = Assert.IsType<SendOtpCodeCommand>(_mockMessageBus.LastPublishedMessage);
        Assert.Equal(phoneNumber, published.PhoneNumber);
    }

    #endregion

    #region Validation Tests

    [Fact]
    public async Task HandleWithNullCommandShouldThrowBadHttpRequestException()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(
            () => _handler.Handle(null!, CancellationToken.None));

        Assert.Equal("Request body cannot be null.", exception.Message);
    }

    [Fact]
    public async Task HandleWithEmptyPhoneNumberShouldThrowValidationException()
    {
        // Arrange
        var command = new LoginCommandBuilder()
            .WithPhoneNumber(string.Empty)
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "PhoneNumber");
    }

    [Fact]
    public async Task HandleWithWhitespacePhoneNumberShouldThrowValidationException()
    {
        // Arrange
        var command = new LoginCommandBuilder()
            .WithPhoneNumber("   ")
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "PhoneNumber");
    }

    [Theory]
    [InlineData("0551234567")]         // starts with 0, fails [1-9] requirement
    [InlineData("abc")]                // non-numeric
    [InlineData("+12345678901234567")] // exceeds 15 digits
    public async Task HandleWithInvalidPhoneNumberFormatShouldThrowValidationException(string phoneNumber)
    {
        // Arrange
        var command = new LoginCommandBuilder()
            .WithPhoneNumber(phoneNumber)
            .Build();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.NotEmpty(exception.Errors);
        Assert.Contains(exception.Errors, e => e.PropertyName == "PhoneNumber");
    }

    [Theory]
    [InlineData("+15551234567")]
    [InlineData("15551234567")]
    [InlineData("+233501234567")]
    public async Task HandleWithValidPhoneNumberFormatsShouldSucceed(string phoneNumber)
    {
        // Arrange
        var command = new LoginCommandBuilder()
            .WithPhoneNumber(phoneNumber)
            .Build();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(phoneNumber, response.PhoneNumber);
    }

    #endregion

    #region Caching Tests

    [Fact]
    public async Task HandleWithSameRequestIdShouldReturnCachedResultOnSecondCall()
    {
        // Arrange
        var requestId = Guid.NewGuid().ToString();
        var command1 = new LoginCommandBuilder()
            .WithRequestId(requestId)
            .Build();

        // Act
        var response1 = await _handler.Handle(command1, CancellationToken.None);

        _mockAuthenticationServices.Reset();
        _mockMessageBus.Reset();

        var command2 = new LoginCommandBuilder()
            .WithRequestId(requestId)
            .Build();
        var response2 = await _handler.Handle(command2, CancellationToken.None);

        // Assert
        Assert.Equal(response1.PhoneNumber, response2.PhoneNumber);
        // Neither service nor bus should be called again after a cache hit
        Assert.Equal(0, _mockAuthenticationServices.GenerateOtpCodeAsyncCallCount);
        Assert.Equal(0, _mockMessageBus.PublishAsyncCallCount);
    }

    [Fact]
    public async Task HandleWithDifferentRequestIdsShouldNotUseCache()
    {
        // Arrange
        var command1 = new LoginCommandBuilder()
            .WithRequestId(Guid.NewGuid().ToString())
            .Build();

        var command2 = new LoginCommandBuilder()
            .WithRequestId(Guid.NewGuid().ToString())
            .Build();

        // Act
        await _handler.Handle(command1, CancellationToken.None);
        await _handler.Handle(command2, CancellationToken.None);

        // Assert
        Assert.Equal(2, _mockAuthenticationServices.GenerateOtpCodeAsyncCallCount);
        Assert.Equal(2, _mockMessageBus.PublishAsyncCallCount);
    }

    #endregion

    #region Cancellation Token Tests

    [Fact]
    public async Task HandleWithCancelledTokenShouldThrowOperationCanceledException()
    {
        // Arrange
        var command = LoginCommandBuilder.CreateValid();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync().ConfigureAwait(true);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            () => _handler.Handle(command, cts.Token));
    }

    #endregion

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }
}
