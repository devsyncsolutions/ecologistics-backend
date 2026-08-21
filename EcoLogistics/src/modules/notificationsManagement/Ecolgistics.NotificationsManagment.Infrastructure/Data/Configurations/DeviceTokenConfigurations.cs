using Ecologistics.Common.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecolgistics.NotificationsManagment.Infrastructure.Data.Configurations;

public sealed class DeviceTokenConfigurations : IEntityTypeConfiguration<DeviceToken>
{
    public void Configure(EntityTypeBuilder<DeviceToken> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("DeviceTokens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(x => x.Token).IsRequired().HasMaxLength(200);

        builder.Property(x => x.Platform)
            .HasConversion(x => x.ToString(), type => Enum.Parse<DevicePlatform>(type))
            .IsRequired();
    }
}