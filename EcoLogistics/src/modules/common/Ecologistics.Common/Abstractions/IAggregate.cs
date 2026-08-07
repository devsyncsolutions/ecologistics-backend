// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Abstractions;

public interface IAggregate<TKey> : IAggregate, IBaseEntity<TKey>
{
}

public interface IAggregate :IBaseEntity
{
    IReadOnlyCollection<IDomainEvents> DomainEvents { get; }
    void ClearDomainEvents();
}
