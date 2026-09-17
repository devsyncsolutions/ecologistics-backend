// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Entities;

public class BalanceThresholdPerDistrict : BaseEntity<BalanceThresholdPerDistrictId>
{
    public Guid DistrictId { get; set; }
    public decimal BalanceThresholdAmount { get; set; } = decimal.Zero;
}
