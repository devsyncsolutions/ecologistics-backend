// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.UseCases.Interfaces;

public interface ITenantManagementDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<Member> Members { get; }
    DbSet<TenantCompanyPurpose> TenantCompanyPurposes { get; }
    DbSet<CompanyPurpose> CompanyPurposes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
