// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate.ValueObjects;

public record RecurringProperties
{
    public RecurringProperties()
    {
    }

    public RecurringProperties(
        int daysIntervalForPickupIfRecurring,
        DateTimeOffset scheduledForPickupAt,
        PickupId  parentPickupId,
        bool isActive)
    {
        DaysIntervalForPickupIfRecurring = daysIntervalForPickupIfRecurring;
        NextScheduledDate = scheduledForPickupAt.AddDays(daysIntervalForPickupIfRecurring);
        ParentPickupId = parentPickupId;
        IsActive = isActive;
    }

    public int DaysIntervalForPickupIfRecurring { get; set; }
    public DateTimeOffset NextScheduledDate { get; set; }
    public PickupId ParentPickupId { get; set; }
    public bool IsActive { get; set; }
}
