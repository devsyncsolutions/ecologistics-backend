// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetInvoices;

public record GetInvoicesQuery(
    int Page,
    int PageSize);

public record InvoiceDto(
    string ReferenceNumber,
    string PaymentCompletedOn,
    string ContentName,
    decimal AmountPayable,
    string Status,
    string InvoiceDownloadLink);
