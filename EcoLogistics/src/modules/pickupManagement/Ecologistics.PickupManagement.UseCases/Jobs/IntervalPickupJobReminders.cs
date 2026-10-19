// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.Jobs;

public class IntervalPickupJobReminders(
    ICronTickerManager<CronTickerEntity> cronTickerManager,
    IMessageBus bus,
    ITextMessagingService textMessagingService)
{
    [TickerFunction("SendRemindersAsync", "*/30 * * * * *")]
    public async Task SendRemindersAsync(
        TickerFunctionContext<IntervalReminderRequest> context,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        IntervalReminderRequest request = context.Request;

        // check the time gab, if within the 30 minutes, send a reminder or close this particular background job
        if (request.ScheduledForPickupAt < DateTime.UtcNow.AddMinutes(-30))
        {
            await cronTickerManager.DeleteAsync(context.Id, cancellationToken).ConfigureAwait(false);
            return;
        }

        UserDetailsResponse collector = await bus
            .InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = request.CollectorMemberId }, cancellationToken)
            .ConfigureAwait(false);

        int minutesLeft = (request.ScheduledForPickupAt - DateTime.UtcNow).Minutes;
        string message = $@"""
                        Hi {collector.FirstName},

                        Pickup with reference #${request.Reference} is scheduled to be picked up in ${minutesLeft} minutes.
                        """;

        Task notificationTask = bus.InvokeAsync(
            new CreateNotificationCommand(request.TriggeredBy, request.CollectorMemberId, "Pickup reminder", message), cancellationToken);
        Task textingTask =  textMessagingService.SendAsync(collector.PhoneNumber, message, null, cancellationToken);

        await Task.WhenAll(notificationTask, textingTask).ConfigureAwait(false);
    }
}
