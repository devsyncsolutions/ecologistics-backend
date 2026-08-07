// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Domain.Commons;

public class CompanyPurpose : BaseEntity<CompanyPurposeId>
{
    public CompanyPurposeType Type { get; set; }
}
