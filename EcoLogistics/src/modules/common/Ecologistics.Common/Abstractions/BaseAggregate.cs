// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.ObjectModel;

namespace Ecologistics.Common.Abstractions;

public abstract class BaseAggregate<TKey> : BaseEntity<TKey>, IAggregate<TKey>
{
    private readonly Collection<IDomainEvents> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvents> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvents domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
