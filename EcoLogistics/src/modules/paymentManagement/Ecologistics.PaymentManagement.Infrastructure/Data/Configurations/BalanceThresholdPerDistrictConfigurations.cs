// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Infrastructure.Data.Configurations;

internal sealed class BalanceThresholdPerDistrictConfigurations : IEntityTypeConfiguration<BalanceThresholdPerDistrict>
{
    public void Configure(EntityTypeBuilder<BalanceThresholdPerDistrict> builder)
    {
        builder.ToTable("BalanceThresholdsPerDistricts");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => new BalanceThresholdPerDistrictId(value));

        builder.Property(b => b.DistrictId)
            .IsRequired();
    }
}
