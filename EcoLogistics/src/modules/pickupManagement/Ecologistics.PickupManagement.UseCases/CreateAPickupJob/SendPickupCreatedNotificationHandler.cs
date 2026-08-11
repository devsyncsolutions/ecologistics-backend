// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public class SendPickupCreatedNotificationHandler(
    ITextMessagingService textMessagingService,
    IMessageBus bus)
{
    public async Task<PickupCreatedNotificationSent> Handle(SendPickupCreatedNotification command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var user = await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = command.CreatedByUserId }, cancellationToken).ConfigureAwait(false);

        // send a compensation event for when user is null

        string message = $@"
Hi! {user?.FirstName} {user?.LastName},

On behalf of Ecologistics we want to notify you that - your pickup job with reference number {command.PickupReference} has been created successfully.
We will notify you when the pickup is successfully matched with a collector.

Thank you for using EcoLogistics!
        ";

        _= await textMessagingService.SendAsync(user?.PhoneNumber ?? string.Empty, message, null, cancellationToken).ConfigureAwait(false);

        return new PickupCreatedNotificationSent(command.SagaId, command.CreatedByUserId);
    }
}
