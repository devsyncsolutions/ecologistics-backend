// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.StartAScheduledJob;

public class PickupStartedEventHandler(
    ITextMessagingService textMessagingService,
    IMessageBus bus)
{
    private const string Title = "Pickup started";

    public async Task Handle(PickupStartedEvent @event, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(@event);

        Task messagingTask =  SendMessageAsync(@event, textMessagingService, bus, cancellationToken);
        Task generatorNotificationTask = SendGeneratorNotificationAsync(@event, bus, cancellationToken);
        Task collectorNotificationTask = SendCollectorNotificationAsync(@event, bus, cancellationToken);

        await Task.WhenAll(messagingTask, generatorNotificationTask, collectorNotificationTask).ConfigureAwait(false);
    }

    private static async Task SendMessageAsync(PickupStartedEvent @event, ITextMessagingService textMessagingService, IMessageBus bus, CancellationToken cancellationToken)
    {
        UserDetailsResponse user = await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery{ UserId = @event.UserId}, cancellationToken).ConfigureAwait(false);

        string message = $"Scheduled pickup with Reference {@event.Reference} has been started. Your collector is on the way.";
        await textMessagingService
            .SendAsync(
                user.PhoneNumber,
                message,
                null,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private static async Task SendGeneratorNotificationAsync(PickupStartedEvent @event, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        string message = $"Scheduled pickup with Reference {@event.Reference} has been started. Your collector is on the way.";

        await bus.InvokeAsync(new CreateNotificationCommand(
            @event.CollectorId,
            @event.UserId,
            Title,
            message), cancellationToken)
            .ConfigureAwait(false);
    }

    private static async Task SendCollectorNotificationAsync(PickupStartedEvent @event, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        string message = $"You have started a Scheduled pickup with Reference {@event.Reference}.";

        await bus.InvokeAsync(new CreateNotificationCommand(
                @event.CollectorId,
                @event.CollectorId,
                Title,
                message), cancellationToken)
            .ConfigureAwait(false);
    }
}
