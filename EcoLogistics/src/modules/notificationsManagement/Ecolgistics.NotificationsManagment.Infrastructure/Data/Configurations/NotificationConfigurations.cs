// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecolgistics.NotificationsManagment.Infrastructure.Data.Configurations;

public sealed class NotificationConfigurations : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("Notifications");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TriggeredBy)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(15);

        builder.HasIndex(x => x.UserId);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Body)
            .IsRequired()
            .HasMaxLength(300);
    }
}
