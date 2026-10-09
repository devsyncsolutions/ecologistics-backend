// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Contracts.Commands;

public record AddAPickupInvoiceCommand(
    Guid PickupJobId,
    decimal Amount,
    Uri? InvoiceUrl,
    string ContentName);
