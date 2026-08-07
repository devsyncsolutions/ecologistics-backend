// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Infrastructure.Data.Configurations;

internal sealed class TenantConfigurations : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(tenantId => tenantId.Value, dbId => new TenantId(dbId));

        builder.Property(x => x.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(12);
        builder.HasIndex(x => x.RegistrationNumber).IsUnique();

        builder.HasIndex(x => x.IsDeleted);

        builder.Property(x => x.CompanyName)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(x => x.CompanyName).IsUnique();

        builder.Property(x => x.Status)
            .HasDefaultValue(TenantStatus.Unverified)
            .HasConversion(x => x.ToString(), type => Enum.Parse<TenantStatus>(type));

        builder.HasMany(x => x.CompanyPurposes)
            .WithOne()
            .HasForeignKey(x => x.TenantId);

        builder.HasMany(x => x.Members)
            .WithOne()
            .HasForeignKey(x => x.TenantId);
    }
}
