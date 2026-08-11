// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Commons.ValueObjects;

public record WeightRange
{
    public decimal Min { get; }
    public decimal Max { get; }

    public WeightRange(decimal min, decimal max)
    {
        if (min < 0)
            throw new BadRequestException("Min Weight cannot be negative.");
        if (max < min)
            throw new BadRequestException("Max must be greater than or equal to Min.");

        Min = min;
        Max = max;
    }
}
