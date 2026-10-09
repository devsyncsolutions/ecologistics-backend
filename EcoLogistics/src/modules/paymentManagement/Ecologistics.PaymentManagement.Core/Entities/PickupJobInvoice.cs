// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Entities;

public class PickupJobInvoice : BaseEntity<PickupJobInvoiceId>
{
    public string ReferenceNumber { get; set; } = string.Empty;
    public decimal AmountPayable { get; set; }
    public decimal Commission { get; set; }
    public DateTime PaymentCompletedOn { get; set; }
    public Uri? InvoiceUrl { get; set; }
    public string ContentName { get; set; } = string.Empty;
    public Guid AssociatedPickupId { get; set; }
    public string CollectorMemberId { get; set; } = string.Empty;
    public InvoiceStatus Status { get; set; }
}
