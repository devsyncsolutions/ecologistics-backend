// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Interfaces;

public interface IPickupServices
{
    Task<string> GenerateReference(string formattedAddress, CancellationToken cancellationToken);
    Task<EligibilityResult> IsEligibleToAcceptAPickup(CancellationToken cancellationToken);
    Task<List<PickupHistoryAsCollectorDto>> GetPickupHistoryAsCollector(
        int page,
        int pageSize,
        PickupType? type,
        Priority? priority,
        string? wasteContentName,
        DateTime fromDate,
        DateTime toDate,
        CancellationToken cancellationToken);
}
