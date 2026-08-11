// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Abstractions;

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public record CreateAPickupJobCommand(
    string PickupFeeId,
    string Type,
    int DaysIntervalForPickupIfRecurring,
    Coordinates Coordinates,
    string FormattedAddress,
    string? GhanaPostGpsAddress,
    string? Landmark,
    string? ContactName,
    string? ContactPhone,
    string? ExtraNotes,
    string ScheduledForPickupAt,
    string WasteContentName,
    string Priority) : BaseRequest;

public record Coordinates(double Latitude, double Longitude);

public record CreateAPickupJobResponse(Guid PickupId);
