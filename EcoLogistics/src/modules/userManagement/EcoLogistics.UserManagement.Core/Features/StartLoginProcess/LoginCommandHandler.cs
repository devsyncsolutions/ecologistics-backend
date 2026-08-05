// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.StartLoginProcess;

public class LoginCommandHandler(
    IAuthenticationServices authenticationServices,
    IMessageBus bus,
    IFusionCache cache)
{
    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
            throw new BadHttpRequestException("Request body cannot be null.", StatusCodes.Status400BadRequest);

        var validation = new LoginCommandValidator();
        var validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var requestResponse = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<LoginCommand>(command.RequestId),
            async _ => await LoginAsync(authenticationServices, bus, command, cancellationToken).ConfigureAwait(false),
            TimeSpan.FromMinutes(30),
            cancellationToken).ConfigureAwait(false);

        return requestResponse;
    }

    private static async Task<LoginResponse> LoginAsync(
        IAuthenticationServices authenticationServices,
        IMessageBus bus,
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        string otpCode = await authenticationServices
            .GenerateOtpCodeAsync(command.PhoneNumber, cancellationToken)
            .ConfigureAwait(false);

        await bus.PublishAsync(new SendOtpCodeCommand(command.PhoneNumber, otpCode, command.RequestId)).ConfigureAwait(false);

        return new LoginResponse(command.PhoneNumber);
    }
}


