// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.Configurations;

internal sealed class PickupFeeConfigurations : IEntityTypeConfiguration<PickupFee>
{
    public void Configure(EntityTypeBuilder<PickupFee> builder)
    {
        builder.ToTable("PickupFees");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(pickupFeeId => pickupFeeId.Value, dbId => new PickupFeeId(dbId));

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.Commission)
            .IsRequired();

        builder.HasOne(x => x.District)
            .WithMany()
            .HasForeignKey(x => x.DistrictId);

        builder.HasOne(x => x.WasteMeasurement)
            .WithMany()
            .HasForeignKey(x => x.WasteMeasurementId);

        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => new { x.DistrictId, x.WasteMeasurementId });
    }
}
