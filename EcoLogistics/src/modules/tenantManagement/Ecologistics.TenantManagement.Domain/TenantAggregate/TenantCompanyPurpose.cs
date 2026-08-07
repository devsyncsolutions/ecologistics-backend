// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Domain.TenantAggregate;

public class TenantCompanyPurpose : BaseEntity<Guid>
{
    protected TenantCompanyPurpose() {}

    internal TenantCompanyPurpose(TenantId tenantId, CompanyPurposeId companyPurposeId)
    {
        ArgumentNullException.ThrowIfNull(tenantId);
        ArgumentNullException.ThrowIfNull(companyPurposeId);

        TenantId = tenantId;
        CompanyPurposeId = companyPurposeId;
    }

    public TenantId TenantId { get; private set; }
    public CompanyPurposeId CompanyPurposeId { get; private set; }
}
