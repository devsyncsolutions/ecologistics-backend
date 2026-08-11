// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Contracts.Queries;

namespace Ecologistics.TenantManagement.UseCases.Tenants.CreateATenantCompany;

public class TenantCreatedEventHandler(
    ITextMessagingService textMessagingService,
    ILogger<TenantCreatedEventHandler> logger,
    IMessageBus bus)
{
    public async Task Handle(TenantCreatedEvent @event, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var creator = await bus
            .InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = @event.CreatedBy }, cancellationToken)
            .ConfigureAwait(false);

        if (creator is null)
        {
            logger.LogErrorMessage(
                "Error sending text message to user: creator not found.",
                null,
                @event.CreatedBy,
                null);
            return;
        }

        var message = $@"
        Hi {creator.FirstName} {creator.LastName},

        On behalf of the Ecologistics team, we are pleased to announce that your company {@event.CompanyName} has been successfully registered.
        Congratulations!
        ";

        await textMessagingService.SendAsync(creator.PhoneNumber, message, null, cancellationToken).ConfigureAwait(false);
    }
}
