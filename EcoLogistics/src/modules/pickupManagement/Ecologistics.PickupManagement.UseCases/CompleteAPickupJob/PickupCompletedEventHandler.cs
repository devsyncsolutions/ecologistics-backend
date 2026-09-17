// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CompleteAPickupJob;

public class PickupCompletedEventHandler(
    IMessageBus bus,
    ITextMessagingService textMessagingService)
{
    public async Task Handle(PickupCompletedEvent @event, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(@event);

        UserDetailsResponse collector =
            await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = @event.CollectorMemberId }, cancellationToken).ConfigureAwait(false);
        UserDetailsResponse generator =
            await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = @event.CreatedBy }, cancellationToken).ConfigureAwait(false);

        const string messageTitle = "Pickup Completed";

        string collectorMessage = $"""
                                   Hello {collector.FirstName} {collector.LastName},
                                   The pickup job with reference {@event.PickupReference} has been completed successfully.

                                   Thank you for using EcoLogistics!.
                                   """;

        string generatorMessage = $"""
                                    Hello {generator.FirstName} {generator.LastName},
                                    The pickup job with reference {@event.PickupReference} has been completed successfully by:

                                    {collector.FirstName} {collector.LastName}
                                    Contact Number: {collector.PhoneNumber}

                                    Thank you for using EcoLogistics!.
                                    """;

        Task sendCollectorMessageTask = textMessagingService.SendAsync(collector.PhoneNumber, collectorMessage, null, cancellationToken);
        Task sendGeneratorMessageTask = textMessagingService.SendAsync(generator.PhoneNumber, generatorMessage, null, cancellationToken);
        Task sendGeneratorNotificationTask = bus.InvokeAsync(new CreateNotificationCommand(
            @event.CollectorMemberId,
            @event.CreatedBy,
            messageTitle,
            generatorMessage), cancellationToken);

        await Task.WhenAll(sendCollectorMessageTask, sendGeneratorMessageTask, sendGeneratorNotificationTask).ConfigureAwait(false);
    }
}
