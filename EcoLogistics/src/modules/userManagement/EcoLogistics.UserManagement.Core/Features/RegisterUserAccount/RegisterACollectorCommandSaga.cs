// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Contracts.Commands;
using Wolverine.Persistence.Sagas;

namespace EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;

public record RegisterACollectorCommand(
    RegisterUserAccountCommand Command) : BaseRequest;
public record UserAccountCreatedCommand(
    [property: SagaIdentity] Guid SagaId,
    string UserId);

public record TenantCompanyCreatedCommand(Guid TenantId);

public record UserAccountCreationFailedCommand(
    [property: SagaIdentity] Guid SagaId,
    string ErrorMessage);

public class RegisterACollectorCommandSaga : Saga
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool UserAccountCreated { get; set; }
    public bool TenantCompanyCreated { get; set; }
    public DateTimeOffset StartedAt { get; set; }

    public static async Task<(
        RegisterACollectorCommandSaga,
        RegisterUserAccountCommand)> Start(
        RegisterACollectorCommand @event,
        ILogger<RegisterACollectorCommandSaga> logger)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var saga = new RegisterACollectorCommandSaga
        {
            Id = Guid.NewGuid(),
            PhoneNumber = @event.Command.PhoneNumber,
            FirstName = @event.Command.FirstName,
            LastName = @event.Command.LastName,
            StartedAt = DateTimeOffset.UtcNow
        };

        var command = @event.Command;
        return (saga, command);
    }

    public CreateATenantCompanyCommand Handle(UserAccountCreatedCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        UserAccountCreated = true;
        return new CreateATenantCompanyCommand($"{FirstName} {LastName}", command.UserId);
    }

    public void Handle(UserAccountCreationFailedCommand command, ILogger<RegisterACollectorCommandSaga> logger)
    {
        ArgumentNullException.ThrowIfNull(command);

        logger.LogErrorMessage(
            $"Tenant company creation failed for {PhoneNumber}, rolling back user account: {command.ErrorMessage}",
            null, PhoneNumber, null);

        MarkCompleted();
    }

    public void Handle(TenantCompanyCreatedCommand command)
    {
        TenantCompanyCreated = true;
        MarkCompleted();
    }
}
