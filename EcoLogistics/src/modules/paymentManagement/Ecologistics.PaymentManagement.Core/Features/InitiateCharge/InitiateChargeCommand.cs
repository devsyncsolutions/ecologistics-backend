// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.InitiateCharge;

public record InitiateChargeCommand(
    string PickupId,
    string? PaymentMethodId,
    NewPaymentMethodDto? NewPaymentMethod) : BaseRequest;

public record NewPaymentMethodDto(
    string MethodType,
    string MobileNumber,
    bool SaveMethod = true);

public record InitiateChargeResponse(string PaymentId, string Status);
