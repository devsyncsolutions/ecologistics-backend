// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Abstractions;

public interface IDomainEvents
{
    Guid EventId => Guid.NewGuid();
    public DateTimeOffset OccuredOn => DateTimeOffset.UtcNow;
    public string? EventType => GetType().AssemblyQualifiedName;
}
