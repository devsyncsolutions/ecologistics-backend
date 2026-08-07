// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Domain.TenantAggregate.Events;

public record TenantStatusUpdatedEvent(
    TenantId TenantId,
    TenantStatus Status) : IDomainEvents;
