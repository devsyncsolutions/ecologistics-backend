// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Dtos;

public record PickupHistoryAsCollectorDto(
    string PickupId,
    string Reference,
    string WasteContentName,
    string CompletedAt,
    decimal Amount,
    decimal Commission
);
