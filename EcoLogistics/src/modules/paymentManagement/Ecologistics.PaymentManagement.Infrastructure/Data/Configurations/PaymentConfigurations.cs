// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Enums;

namespace Ecologistics.PaymentManagement.Infrastructure.Data.Configurations;

internal sealed class PaymentConfigurations : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(p => p.Value, p => new PaymentId(p));

        builder.Property(p => p.AssociatedPickupId)
            .IsRequired();
        builder.Property(p => p.GeneratorId)
            .IsRequired()
            .HasMaxLength(15);
        builder.Property(p => p.CollectorMemberId)
            .IsRequired()
            .HasMaxLength(15);

        builder.HasIndex(p => p.AssociatedPickupId);

        builder.Property(p => p.PaymentMethodId)
            .HasConversion(p => p!.Value, p => new UserPaymentMethodId(p));
        builder.Property(p => p.PendingPaymentMethodId)
            .HasConversion(p => p!.Value, p => new UserPaymentMethodId(p));

        builder.ComplexProperty(p => p.AmountPayedByGenerator, amount =>
        {
            amount.Property(p => p.Amount)
                .HasColumnName("Amount");
            amount.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasConversion(c => c.ToString(), currencies => Enum.Parse<Currency>(currencies));
        });

        builder.ComplexProperty(p => p.Commission, amount =>
        {
            amount.Property(p => p.Amount)
                .HasColumnName("Amount");
            amount.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasConversion(c => c.ToString(), currencies => Enum.Parse<Currency>(currencies));
        });

        builder.ComplexProperty(p => p.AmountPayedToCollectorTenant, amount =>
        {
            amount.Property(p => p.Amount)
                .HasColumnName("Amount");
            amount.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasConversion(c => c.ToString(), currencies => Enum.Parse<Currency>(currencies));
        });

        builder.Property(p => p.Status)
            .HasConversion(p => p.ToString(), p => Enum.Parse<PaymentStatus>(p));

        builder.Property(p => p.PaystackReference)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(p => p.PaystackTransactionId)
            .HasMaxLength(15);
    }
}
