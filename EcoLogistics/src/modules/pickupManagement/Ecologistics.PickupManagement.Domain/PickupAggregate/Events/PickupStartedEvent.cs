// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate.Events;

public record PickupStartedEvent(PickupId Id, string Reference, string UserId, string CollectorId, PickupType Type) : IDomainEvents;
