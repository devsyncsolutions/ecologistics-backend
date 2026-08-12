// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;

public record RegisterUserAccountCommand(
    string FirstName,
    string LastName,
    string? OtherNames,
    string Gender,
    string PhoneNumber,
    string DistrictId,
    AccountType AccountType) : BaseRequest;

public record RegisterUserAccountRequest(
    string FirstName,
    string LastName,
    string? OtherNames,
    string Gender,
    string PhoneNumber,
    string DistrictId);

public record RegisterUserAccountResponse(string UserId);
