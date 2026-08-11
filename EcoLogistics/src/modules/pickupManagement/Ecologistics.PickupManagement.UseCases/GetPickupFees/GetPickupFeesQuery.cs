// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupFees;

public record GetPickupFeesQuery;

public record GetPickupFeesResponse(
    string PickupFeeId,
    string DistrictName,
    MeasurementDto Measurement,
    decimal Amount,
    decimal Commission);

public record MeasurementDto(
    string Name,
    string UnitOfMeasurement,
    string Description,
    decimal Min,
    decimal Max);
