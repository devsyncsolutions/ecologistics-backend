// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Events;

public record PaymentInitiatedEvent(
    Guid PaymentId) : IDomainEvents;
