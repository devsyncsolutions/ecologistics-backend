// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Infrastructure.Data.Configurations;

internal sealed class PickupJobInvoiceConfigurations : IEntityTypeConfiguration<PickupJobInvoice>
{
    public void Configure(EntityTypeBuilder<PickupJobInvoice> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("PickupJobInvoices");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, y => new PickupJobInvoiceId(y));

        builder.Property(x => x.ReferenceNumber)
            .IsRequired()
            .HasMaxLength(13);
        builder.HasIndex(x => x.ReferenceNumber);

        builder.Property(x => x.ContentName)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.CollectorMemberId)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(InvoiceStatus.Unpaid);

        builder.HasIndex(x => x.CreatedBy);
        builder.HasIndex(x => x.CollectorMemberId);

        builder.Property(x => x.AssociatedPickupId)
            .IsRequired();
    }
}
