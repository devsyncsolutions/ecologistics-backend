// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Commons;

public class WasteMeasurement : BaseEntity<WasteMeasurementId>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WeightRange WeightRange { get; set; }
    public string UnitOfMeasurement { get; set; } = string.Empty;
}
