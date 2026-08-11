// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.Configurations;

internal sealed class WasteMeasurementConfigurations : IEntityTypeConfiguration<WasteMeasurement>
{
    public void Configure(EntityTypeBuilder<WasteMeasurement> builder)
    {
        builder.ToTable("WasteMeasurements");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(wasteMeasurementId => wasteMeasurementId.Value, dbId => new WasteMeasurementId(dbId));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.IsDeleted);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.ComplexProperty(x => x.WeightRange, wr =>
        {
            wr.Property(w => w.Min)
                .HasColumnName("WeightRangeMin ")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            wr.Property(w => w.Max)
                .HasColumnName("WeightRangeMax ")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        });

        builder.Property(x => x.UnitOfMeasurement)
            .IsRequired()
            .HasMaxLength(4);
    }
}
