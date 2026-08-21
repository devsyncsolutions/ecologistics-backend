// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.UpdatePickup;

public record UpdatePickupCommand(
    string? ContactName,
    string? ContactPhone,
    string? ExtraNotes,
    string? ScheduledForPickupAt,
    string? Priority,
    string? WasteContentName)
{
    public Guid Id { get; set; }
}
