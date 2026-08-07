// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Contracts;

public record GetUserDetailsQuery
{
    public string? UserId { get; set; }
    public string? PhoneNumber { get; set; }
};

public record UserDetailsResponse(
    string FirstName,
    string LastName,
    string? OtherNames,
    string PhoneNumber,
    string DateOfBirth,
    string Gender,
    string ProfileImage,
    string Residence,
    string ResidenceGps);
