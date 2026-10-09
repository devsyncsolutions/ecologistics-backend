// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Entities;

public class Payment : BaseAggregate<PaymentId>
{
    public Guid AssociatedPickupId { get; private set; }
    public string GeneratorId { get; private set; } = string.Empty;
    public string CollectorMemberId { get; private set; } = string.Empty;
    public UserPaymentMethodId? PaymentMethodId { get; private set; }
    public UserPaymentMethodId? PendingPaymentMethodId { get; private set; }
    public Money AmountPayedByGenerator { get; private set; }
    public Money Commission { get; private set; }
    public Money AmountPayedToCollectorTenant { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string PaystackReference { get; private set; } = string.Empty;
    public string? PaystackTransactionId { get; private set; }

    private Payment() {}

    public static Payment Create(Guid pickupId, string generator, string collector,
        UserPaymentMethodId? paymentMethodId, Money amount, Money commission, bool isCashPayment = false)
    {
        ArgumentNullException.ThrowIfNull(amount);
        ArgumentNullException.ThrowIfNull(commission);

        decimal amountPayedToTenant = amount.Amount - commission.Amount;
        Payment newPayment = new()
        {
            Id = new PaymentId(Guid.CreateVersion7()),
            AssociatedPickupId = pickupId,
            GeneratorId = generator,
            CollectorMemberId = collector,
            PaymentMethodId = paymentMethodId,
            PendingPaymentMethodId = null,
            AmountPayedByGenerator = amount,
            Commission = commission,
            AmountPayedToCollectorTenant = new Money(Currency.GHS, amountPayedToTenant),
            Status = isCashPayment ? PaymentStatus.CashConfirmed : PaymentStatus.Initiated,
            PaystackReference = $"eco_{pickupId:N}_{DateTime.UtcNow.Ticks}",
            PaystackTransactionId = string.Empty
        };

        if(!isCashPayment)
            newPayment.AddDomainEvent(new PaymentInitiatedEvent(newPayment.Id.Value));

        return newPayment;
    }

    public void UpdateStatus(PaymentStatus status, string? paystackTransactionId = null)
    {
        PaystackTransactionId = status switch
        {
            PaymentStatus.Success when string.IsNullOrWhiteSpace(paystackTransactionId) =>
                throw new ArgumentException("Paystack transaction id cannot be null or empty.", nameof(paystackTransactionId)),
            PaymentStatus.Success => paystackTransactionId,
            _ => PaystackTransactionId
        };

        Status = status;

        AddDomainEvent(new PaymentStatusUpdatedEvent(Id.Value, status));
    }

    public void LinkPendingPaymentMethodId(UserPaymentMethodId? paymentMethodId)
    {
        ArgumentNullException.ThrowIfNull(paymentMethodId);

        PendingPaymentMethodId = paymentMethodId;
        AddDomainEvent(new PendingPaymentMethodUpdatedEvent(GeneratorId, paymentMethodId));
    }
}
