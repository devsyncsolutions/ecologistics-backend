// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;

public class RegisterUserAccountCommandHandler(
    IAuthenticationServices authenticationService, IFusionCache cache, IMessageBus bus)
{
    public async Task<RegisterUserAccountResponse> Handle(RegisterUserAccountCommand command,
        CancellationToken cancellationToken)
    {
        if(command is null)
            throw new BadHttpRequestException("Command cannot be null", StatusCodes.Status400BadRequest);

        var validation = new RegisterUserAccountCommandValidator();
        var validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var requestResponse = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<RegisterUserAccountCommand>(command.RequestId),
            async _ => await RegisterUserAccountAsync(authenticationService, command, bus, cache,  cancellationToken).ConfigureAwait(false),
            TimeSpan.FromMinutes(30),
            cancellationToken).ConfigureAwait(false);

        return requestResponse;
    }

    private static async Task<RegisterUserAccountResponse> RegisterUserAccountAsync(
        IAuthenticationServices authenticationService,
        RegisterUserAccountCommand command,
        IMessageBus bus,
        IFusionCache cache,
        CancellationToken cancellationToken)
    {
        GetDistrictResponse existingDistrict = await bus
            .InvokeAsync<GetDistrictResponse>(new GetDistrictQuery(command.DistrictId), cancellationToken)
            .ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(existingDistrict.DistrictId))
        {
            await cache.RemoveAsync(CacheKeys.GetRequestKey<RegisterUserAccountCommand>(command.RequestId),
                token: cancellationToken).ConfigureAwait(false);
            throw new NotFoundException($"District with ID {command.DistrictId} not found.");
        }

        var applicationUser = new ApplicationUser
        {
            UserName = command.PhoneNumber,
            PhoneNumber = command.PhoneNumber,
            FirstName = command.FirstName,
            LastName = command.LastName,
            OtherNames = command.OtherNames,
            DistrictId = Guid.Parse(command.DistrictId),
            Gender = Enum.Parse<Gender>(command.Gender)
        };

        await authenticationService.RegisterAsync(applicationUser, command.AccountType, cancellationToken).ConfigureAwait(false);

        return new RegisterUserAccountResponse(command.PhoneNumber);
    }
}
