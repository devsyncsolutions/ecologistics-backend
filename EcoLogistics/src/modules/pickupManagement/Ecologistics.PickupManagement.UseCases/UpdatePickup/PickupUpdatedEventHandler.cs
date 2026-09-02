// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.UpdatePickup;

public record NotifyCollectorMemberAboutUpdatedPickupCommand(
    string PickupReference,
    string CollectorMemberId);

public class PickupUpdatedEventHandler(
    IPickupManagementDbContext dbContext)
{
    public async Task<NotifyCollectorMemberAboutUpdatedPickupCommand> Handle(PickupUpdatedEvent @event, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var pickup = await dbContext
            .Pickups
            .Where(p => p.Id == @event.Id)
            .Select(p => new
            {
                p.Id,
                p.Reference,
                p.CollectorMemberId,
                p.TimeStamp.ScheduledForPickupAt,
                p.Priority,
                p.WasteContentName,
                p.PickupStatus
            })
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickup.PickupStatus == PickupStatus.Scheduled)
        {
            await dbContext
                .PickupsForQuerying
                .Where(pq => pq.PickupId == pickup.Id.Value)
                .ExecuteUpdateAsync(setter =>
                        setter
                            .SetProperty(pq => pq.ContentName, pickup.WasteContentName)
                            .SetProperty(pq => pq.Priority, pickup.Priority.ToString()),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        return new NotifyCollectorMemberAboutUpdatedPickupCommand(pickup.Reference, pickup.CollectorMemberId ?? string.Empty);
    }
}
