// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.Configurations;

internal sealed class WasteKindConfigurations : IEntityTypeConfiguration<WasteKind>
{
    public void Configure(EntityTypeBuilder<WasteKind> builder)
    {
        builder.ToTable("WasteKinds");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(wasteKindId => wasteKindId.Value, dbId => new WasteKindId(dbId));
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => new {x.Name, x.CreatedAt}).IsUnique();
    }
}
