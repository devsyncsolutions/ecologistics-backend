// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TickerQ.Utilities.Enums;

namespace Ecologistics.PickupManagement.UseCases.Jobs;

public record IntervalReminderRequest(
    string Reference,
    DateTimeOffset ScheduledForPickupAt,
    string CollectorMemberId,
    string TriggeredBy);

// Get all pickupJobs scheduled for today
// Set up recurring background jobs for each pickupJob
// With 30-minutes interval

public class DailyPickupJobReminders(
    IPickupManagementDbContext dbContext,
    ICronTickerManager<CronTickerEntity> cronTickerManager)
{
    [TickerFunction(
        "FetchAndSetupReminders",
        "0 6 * * *",
        TickerTaskPriority.LongRunning)]
    public async Task FetchAndSetupReminders(CancellationToken cancellationToken)
    {
        var pickupJobsDueToday = await dbContext
            .Pickups
            .Where(p =>
                p.TimeStamp.ScheduledForPickupAt.Date == DateTime.Today &&
                p.PickupStatus == PickupStatus.Matched)
            .Select(p => new
            {
                p.Id,
                p.CollectorMemberId,
                p.Reference,
                p.TimeStamp.ScheduledForPickupAt,
                p.CreatedBy
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickupJobsDueToday.Count == 0)
            return;

        List<CronTickerEntity> allJobs =
        [
            .. pickupJobsDueToday.Select(pickup => new CronTickerEntity
            {
                Function = "SendRemindersAsync",
                Expression = "*/30 * * * *",
                IsEnabled  = true,
                Request = TickerHelper.CreateTickerRequest(new IntervalReminderRequest(
                        pickup.Reference,
                        pickup.ScheduledForPickupAt,
                        pickup.CollectorMemberId!,
                        pickup.CreatedBy!)),
                Retries = 3,
                RetryIntervals = [5, 10, 15]
            })
        ];

        await cronTickerManager.AddBatchAsync(allJobs, cancellationToken).ConfigureAwait(false);
    }
}
