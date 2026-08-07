// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Infrastructure.Data.Configurations;

internal sealed class TenantCompanyPurposeConfigurations :IEntityTypeConfiguration<TenantCompanyPurpose>
{
    public void Configure(EntityTypeBuilder<TenantCompanyPurpose> builder)
    {
        builder.ToTable("TenantCompanyPurposes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId)
            .HasConversion(tenantId => tenantId.Value, dbId => new TenantId(dbId))
            .IsRequired();

        builder.Property(x => x.CompanyPurposeId)
            .HasConversion(companyPurposeId => companyPurposeId.Value, dbId => new CompanyPurposeId(dbId))
            .IsRequired();

        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => x.CompanyPurposeId);
    }
}
