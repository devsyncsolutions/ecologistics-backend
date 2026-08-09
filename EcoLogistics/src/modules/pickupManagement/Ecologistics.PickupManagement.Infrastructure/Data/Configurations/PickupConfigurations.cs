// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.Configurations;

internal sealed class PickupConfigurations : IEntityTypeConfiguration<Pickup>
{
    public void Configure(EntityTypeBuilder<Pickup> builder)
    {
        builder.ToTable("Pickups");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(pickupId => pickupId.Value, dbId => new PickupId(dbId));

        builder.HasIndex(x => x.IsDeleted);

        builder.Property(x => x.Type)
            .HasDefaultValue(PickupType.OneTime)
            .HasConversion(x => x.ToString(), type => Enum.Parse<PickupType>(type))
            .IsRequired();

        builder.Property(x => x.Coordinates)
            .IsRequired()
            .HasColumnType("geography (point)")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(x => x.Coordinates)
            .HasMethod("GIST");

        builder.Property(x => x.FormattedAddress)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.GhanaPostGpsAddress)
            .HasMaxLength(15);

        builder.Property(x => x.Landmark)
            .HasMaxLength(255);

        builder.Property(x => x.ContactName)
            .HasMaxLength(50);

        builder.Property(x => x.ContactPhone)
            .HasMaxLength(15);

        builder.Property(x => x.ExtraNotes)
            .HasMaxLength(500);

        builder.ComplexProperty(x => x.TimeStamp, ts =>
        {
            ts.Property(t => t.ScheduledForPickupAt)
                .HasColumnName("ScheduledForPickupAt")
                .IsRequired();

            ts.Property(t => t.JobStartedAt)
                .HasColumnName("JobStartedAt");

            ts.Property(t => t.JobCompletedAt)
                .HasColumnName("JobCompletedAt");
        });

        builder.Property(x => x.PickupFeeId)
            .HasConversion(id => id.Value, dbId => new PickupFeeId(dbId))
            .IsRequired();

        builder.Property(x => x.PickupStatus)
            .HasConversion(x => x.ToString(), type => Enum.Parse<PickupStatus>(type))
            .HasDefaultValue(PickupStatus.Scheduled)
            .IsRequired();
        builder.HasIndex(x => x.PickupStatus);

        builder.Property(x => x.CollectorMemberId)
            .HasMaxLength(20);
        builder.HasIndex(x => x.CollectorMemberId);
        builder.HasIndex(x => x.CreatedBy);

        builder.ComplexProperty(x => x.IsCancelled, isCancelled =>
        {
            isCancelled.Property(x => x.CancelledAt)
                .HasColumnName("CancelledAt");

            isCancelled.Property(x => x.CancelledBy)
                .HasColumnName("CancelledBy")
                .HasMaxLength(20);

            isCancelled.Property(x => x.Reason)
                .HasColumnName("Reason")
                .HasMaxLength(500);
        });

        builder.Property(x => x.Reference)
            .HasMaxLength(7)
            .IsRequired();
        builder.HasIndex(x => x.Reference).IsUnique();

        builder.Property(x => x.WasteContentName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired()
            .HasDefaultValue(Priority.High)
            .HasConversion(x => x.ToString(), type => Enum.Parse<Priority>(type));
    }
}
