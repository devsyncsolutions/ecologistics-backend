// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupJobs;

public record GetPickupJobsQuery(
    int Page,
    int PageSize,
    string Type,
    string Priority,
    double Latitude,
    double Longitude,
    double RadiusInMeters);

public record PickupJobDto(
    string PickupId,
    string Reference,
    string ContentName,
    string FormattedAddress,
    string Type,
    decimal Amount,
    decimal Commission,
    string AverageWeight,
    string CreatedAt);
