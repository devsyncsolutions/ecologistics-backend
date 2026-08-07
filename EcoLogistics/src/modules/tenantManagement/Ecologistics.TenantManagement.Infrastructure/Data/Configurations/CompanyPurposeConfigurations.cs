// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Infrastructure.Data.Configurations;

internal sealed class CompanyPurposeConfigurations : IEntityTypeConfiguration<CompanyPurpose>
{
    public void Configure(EntityTypeBuilder<CompanyPurpose> builder)
    {
        builder.ToTable("CompanyPurposes");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(memberId => memberId.Value, dbId => new CompanyPurposeId(dbId));

        builder.Property(x => x.Type)
            .HasDefaultValue(CompanyPurposeType.Collector)
            .HasConversion(x => x.ToString(), type => Enum.Parse<CompanyPurposeType>(type));
    }
}
