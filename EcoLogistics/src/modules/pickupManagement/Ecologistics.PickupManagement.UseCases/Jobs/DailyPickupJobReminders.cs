// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TickerQ.Utilities;

namespace Ecologistics.PickupManagement.UseCases.Jobs;

public record IntervalReminderRequest(
    Guid PickupId,
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
    [TickerFunction("DailyPickupJobReminders", "0 0 1 * * *")]
    public async Task FetchAndSetupReminders(TickerFunctionContext context, CancellationToken cancellationToken)
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
                Function = nameof(IntervalPickupJobReminders.SendRemindersAsync),
                Expression = "*/30 * * * *",
                IsEnabled  = true,
                Request = TickerHelper.CreateTickerRequest(new IntervalReminderRequest(
                        pickup.Id.Value,
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
