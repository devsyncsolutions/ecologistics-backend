// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.Jobs;

public class IntervalPickupJobReminders(
    ICronTickerManager<CronTickerEntity> cronTickerManager)
{
    public async Task SendRemindersAsync(
        TickerFunctionContext<IntervalReminderRequest> context,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        var request = context.Request;

        // check the time gab, if within the 30 minutes, send a reminder or close this particular background job
        if (request.ScheduledForPickupAt < DateTime.UtcNow.AddMinutes(-30))
        {
            await cronTickerManager.DeleteAsync(context.Id, cancellationToken).ConfigureAwait(false);

        }

    }
}
