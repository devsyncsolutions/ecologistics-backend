// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate.ValueObjects;

public record TimeStamp
{
    public DateTimeOffset ScheduledForPickupAt { get; set; }
    public DateTimeOffset? JobStartedAt { get; set; }
    public DateTimeOffset? JobCompletedAt { get; set; }
}
