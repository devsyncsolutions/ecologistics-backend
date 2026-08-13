// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.Configurations;

internal sealed class PickupForQueryConfigurations : IEntityTypeConfiguration<PickupForQuery>
{
    public void Configure(EntityTypeBuilder<PickupForQuery> builder)
    {
        builder.ToTable("PickupsForQuerying");
        builder.HasKey(x => x.PickupId);

        builder.Property(x => x.PickupId)
            .ValueGeneratedNever();

        builder.Property(x => x.Reference)
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(x => x.ContentName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x  => x.FormattedAddress)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasMaxLength(9)
            .IsRequired();

        builder.Property(x => x.Priority)
            .HasMaxLength(6)
            .IsRequired();

        builder.HasIndex(x => new { x.Type, x.Priority });
        builder.HasIndex(x => x.Coordinates)
            .HasMethod("GIST");

        builder.Property(x => x.AverageWeight)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Coordinates)
            .IsRequired()
            .HasColumnType("geography (point)")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(x => x.NumberOfContainers)
            .IsRequired()
            .HasDefaultValue(1);
    }
}
