// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Commons;

public class PickupFee : BaseEntity<PickupFeeId>
{
    public DistrictId DistrictId { get; set; }
    public District? District { get; set; }

    public WasteMeasurementId WasteMeasurementId { get; set; }
    public WasteMeasurement? WasteMeasurement { get; set; }

    public decimal Amount { get; set; }
    public decimal Commission { get; set; }
}
