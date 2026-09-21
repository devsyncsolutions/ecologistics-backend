// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.EndLoginProcess;

public class GetTokenCommandHandler(
    IAuthenticationServices authenticationServices, 
    IFusionCache cache,
    IMessageBus bus)
{
    public async Task<GetTokenResponse> Handle(GetTokenCommand command, CancellationToken cancellationToken)
    {
        if (command is null)
            throw new BadHttpRequestException("Command cannot be null", StatusCodes.Status400BadRequest);

        var validation = new GetTokenCommandValidator();
        var validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var requestResponse = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<GetTokenCommand>(command.RequestId),
            async _ => await GetTokenAsync(authenticationServices, command, bus, cancellationToken).ConfigureAwait(false),
            TimeSpan.FromMinutes(30),
            cancellationToken).ConfigureAwait(false);

        return requestResponse;
    }

    private static async Task<GetTokenResponse> GetTokenAsync(
        IAuthenticationServices authenticationServices,
        GetTokenCommand command,
        IMessageBus bus,
        CancellationToken cancellationToken)
    {
        var (accessToken, refreshToken) = await authenticationServices
            .AuthenticateAsync(command.PhoneNumber, command.OtpCode, cancellationToken)
            .ConfigureAwait(false);

        if(!string.IsNullOrWhiteSpace(command.DeviceTokenForNotificationBroadcasting) 
            && !string.IsNullOrWhiteSpace(command.Platform))
        {
            await bus.PublishAsync(new CreateDeviceTokenCommand(
                command.PhoneNumber,
                command.DeviceTokenForNotificationBroadcasting,
                Enum.Parse<DevicePlatform>(command.Platform)
            )).ConfigureAwait(false);
        }

        return new GetTokenResponse(accessToken, refreshToken);
    }
}
