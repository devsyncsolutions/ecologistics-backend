// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate.ValueObjects;

public record TimeStamp
{
    public TimeStamp(DateTimeOffset scheduledForPickupAt)
    {
        ScheduledForPickupAt = scheduledForPickupAt;
    }

    public TimeStamp(DateTimeOffset scheduledForPickupAt, DateTimeOffset? jobStartedAt)
    {
        if (jobStartedAt == null)
            throw new BadRequestException("JobStartedAt cannot be null.");

        if(scheduledForPickupAt != ScheduledForPickupAt)
            throw new  BadRequestException("ScheduledAt cannot be changed.");

        // jobStartedAt cannot be equal to or in the past of scheduledForPickupAt.
        if (jobStartedAt < scheduledForPickupAt)
            throw new BadRequestException("JobStartedAt cannot be in the past.");

        JobStartedAt = jobStartedAt;
    }

    public TimeStamp(DateTimeOffset scheduledForPickupAt, DateTimeOffset? jobStartedAt, DateTimeOffset? jobCompletedAt)
        : this(scheduledForPickupAt, jobStartedAt)
    {
        if (jobCompletedAt == null)
            throw new BadRequestException("JobCompletedAt cannot be null.");

        // jobCompletedAt cannot be equal to or in the past of jobStartedAt.
        if (jobCompletedAt < jobStartedAt)
            throw new BadRequestException("JobCompletedAt cannot be in the past.");

        JobCompletedAt = jobCompletedAt;
    }

    public DateTimeOffset ScheduledForPickupAt { get; set; }
    public DateTimeOffset? JobStartedAt { get; set; }
    public DateTimeOffset? JobCompletedAt { get; set; }
}
