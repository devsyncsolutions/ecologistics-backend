// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Enums;

public enum PaymentStatus
{
    Initiated,
    Processing,
    PendingOtp,
    Success,
    Failed,
    CashPendingConfirmation,
    CashConfirmed
}
