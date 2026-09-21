// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Entities;

public class UserPaymentMethod : BaseAggregate<UserPaymentMethodId>
{
    public PaymentMethodType Method { get; set; }
    public PaymentMethodStatus Status { get; set; }

    // Null for Cash. Populated only after a successful, reusable charge.
    public string? PaystackAuthorizationCode { get; set; }
    public string? PaystackSignature { get; set; }
    public string? Last4 { get; set; }
    public CardType? CardType { get; set; }
    public string? CustomerEmail { get; set; }

    public bool IsDefault { get; set; }
}
