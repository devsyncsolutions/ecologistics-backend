// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetPaymentDetails;

public record GetPaymentDetailsQuery(Guid PaymentId);

public record PaymentDetailsDto(
    string Id,
    string AssociatedPickupId,
    string PaymentStatus,
    string GeneratorId,
    string CollectorMemberId,
    string PaymentMethodType,
    MoneyDto AmountPayedByGenerator,
    MoneyDto Commission,
    MoneyDto AmountPayedToCollectorTenant);

public record MoneyDto(decimal Amount, string Currency);
