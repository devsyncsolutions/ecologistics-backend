// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Domain.TenantAggregate;

public class Member : BaseEntity<MemberId>
{
    protected Member() {}

    internal Member(TenantId tenantId, bool isOwner, string userId)
    {
        ArgumentNullException.ThrowIfNull(tenantId);
        ArgumentNullException.ThrowIfNull(userId);

        IsOwner = isOwner;
        TenantId = tenantId;
        UserId = userId;
    }

    public bool IsOwner { get; private set; }
    public TenantId TenantId { get; private set; }
    public string UserId { get; private set; }
}
