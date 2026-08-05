// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.UpdateUserDetails;

public record UpdateUserDetailsCommand(
    string FirstName,
    string LastName,
    string? OtherNames,
    string Gender,
    string? DateOfBirth,
    string? Residence,
    string? ResidenceGps);
public record UpdateUserDetailsResponse(string UserId);
