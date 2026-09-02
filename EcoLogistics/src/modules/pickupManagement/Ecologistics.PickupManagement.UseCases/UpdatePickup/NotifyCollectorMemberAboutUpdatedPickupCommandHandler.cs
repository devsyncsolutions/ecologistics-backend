// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.UpdatePickup;

public class NotifyCollectorMemberAboutUpdatedPickupCommandHandler(
    IMessageBus bus,
    ITextMessagingService textMessagingService)
{
    public async Task Handle(NotifyCollectorMemberAboutUpdatedPickupCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.CollectorMemberId))
            return;

        UserDetailsResponse collector = await bus
            .InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = command.CollectorMemberId },
                cancellationToken)
            .ConfigureAwait(false);

        string message = $"""
                          Hi ${collector.FirstName},

                          Your pickup with Reference {command.PickupReference} has been updated.
                          """;

        await textMessagingService.SendAsync(collector.PhoneNumber, message, null, cancellationToken).ConfigureAwait(false);
    }
}
