// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Infrastructure.Data.Configurations;

public sealed class UserPaymentMethodConfigurations : IEntityTypeConfiguration<UserPaymentMethod>
{
    public void Configure(EntityTypeBuilder<UserPaymentMethod> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("UserPaymentMethods");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new UserPaymentMethodId(value));

        builder.Property(x => x.Method)
            .HasConversion(
                method => method.ToString(),
                value => Enum.Parse<PaymentMethodType>(value))
            .IsRequired()
            .HasDefaultValue(PaymentMethodType.Cash);

        builder.Property(x => x.Status)
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<PaymentMethodStatus>(value))
            .IsRequired()
            .HasDefaultValue(PaymentMethodStatus.PendingVerification);

        builder.Property(x => x.CardType)
            .HasConversion(cardType => cardType.ToString(), value => Enum.Parse<CardType>(value ?? ""));

        builder.Property(x => x.PaystackAuthorizationCode)
            .HasMaxLength(256);

        builder.Property(x => x.IsDefault)
            .HasDefaultValue(false);

        builder.HasIndex(x => x.CreatedBy);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => new { x.CreatedBy, x.PaystackSignature }).IsUnique()
            .HasFilter("\"PaystackSignature\" IS NOT NULL");
    }
}
