// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate;

public class PickupForQuery
{
    public Guid PickupId { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string ContentName { get; set; } = string.Empty;
    public string FormattedAddress { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Commission { get; set; }
    public string AverageWeight { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public Point Coordinates { get; set; } = default!;
}
