// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupDetails;

public record GetPickupDetailsQuery(Guid PickupId);

public record PickupDetailsResponse(
string Type,
string FormattedAddress,
string GhanaPostGpsAddress,
string Landmark,
string ContactName,
string ContactPhone,
string ExtraNotes,
TimeStamp TimeLines,
PickupFeeDto PickupFee,
string PickupStatus,
string CollectorId,
string DownloadableInvoiceLink,
string? CancelledAt,
string? ReasonForCancelling,
string? CancelledBy,
string PickupId,
string Reference,
string WasteContentName,
string Priority,
string? NextScheduledDate,
string CreatedAt,
string CreatedBy,
double Longitude,
double Latitude,
UserDto? Collector,
UserDto Generator);
public record TimeStamp(
    string ScheduledForPickupAt,
    string? JobStartedAt,
    string? JobCompletedAt);

public record PickupFeeDto(
    decimal Amount,
    decimal Commission,
    int NumberOfContainers,
    string NameOfWeight,
    string Weight);

public record UserDto(string PhoneNumber, string FullName, string ProfileImage);
