// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate.ValueObjects;

public record IsCancelled(
    DateTimeOffset CancelledAt,
    string Reason,
    string? CancelledBy);
