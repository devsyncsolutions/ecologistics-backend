// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupCreatedEvent(
    PickupId Id,
    string Reference,
    string? ContactName,
    string? FormattedAddress,
    string Type,
    string Priority,
    Point Coordinates,
    int NumberOfContainers,
    PickupFeeId PickupFeeId,
    string CreatedBy) : IDomainEvents;
