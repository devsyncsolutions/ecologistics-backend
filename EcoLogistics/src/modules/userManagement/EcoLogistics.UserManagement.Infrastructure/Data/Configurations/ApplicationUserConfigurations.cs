// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Infrastructure.Data.Configurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(x => x.UserName).HasMaxLength(15).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired();
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
        builder.HasIndex(x => x.UserName).IsUnique();
        builder.HasIndex(x => x.IsDeleted);

        builder.Property(x => x.FirstName)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(x => x.OtherNames)
            .HasMaxLength(50);

        builder.Property(x => x.Gender)
            .HasConversion(x => x.ToString(), type => Enum.Parse<Gender>(type))
            .IsRequired();

        builder.Property(x => x.Residence)
            .HasMaxLength(100);

        builder.Property(x => x.ResidenceGps)
            .HasMaxLength(13);
    }
}
