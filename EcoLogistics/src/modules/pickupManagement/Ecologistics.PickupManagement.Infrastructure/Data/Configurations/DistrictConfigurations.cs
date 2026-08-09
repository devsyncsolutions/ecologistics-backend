// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.Configurations;

internal sealed class DistrictConfigurations : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable("Districts");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(districtId => districtId.Value, dbId => new DistrictId(dbId));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Region)
            .HasDefaultValue(Region.GreaterAccra)
            .HasConversion(x => x.ToString(), type => Enum.Parse<Region>(type));

        builder.HasIndex(x => x.IsDeleted);
    }
}
