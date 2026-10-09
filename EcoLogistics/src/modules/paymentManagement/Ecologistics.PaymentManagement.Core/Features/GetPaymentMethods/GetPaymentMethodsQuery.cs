// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetPaymentMethods;

public record GetPaymentMethodsQuery;

public record UserPaymentMethodDto(
    string Id,
    string MethodType,
    string Status);
