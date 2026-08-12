// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcoLogistics.UserManagement.Core.Enums;
using EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;

namespace EcoLogistics.UserManagement.Tests.Mock;

/// <summary>
/// Builder pattern for creating RegisterUserAccountCommand instances with flexibility.
/// Useful for test setup with various configurations.
/// </summary>
internal sealed class RegisterUserAccountCommandBuilder
{
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string? _otherNames;
    private string _gender = "Male";
    private string _phoneNumber = "0501234567";
    private string _requestId = Guid.NewGuid().ToString();
    private readonly string _districtId = "133e4567-e89b-12d3-a456-426655440000";

    public RegisterUserAccountCommandBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public RegisterUserAccountCommandBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public RegisterUserAccountCommandBuilder WithOtherNames(string? otherNames)
    {
        _otherNames = otherNames;
        return this;
    }

    public RegisterUserAccountCommandBuilder WithGender(string gender)
    {
        _gender = gender;
        return this;
    }

    public RegisterUserAccountCommandBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public RegisterUserAccountCommandBuilder WithRequestId(string requestId)
    {
        _requestId = requestId;
        return this;
    }

    public RegisterUserAccountCommand Build()
    {
        return new RegisterUserAccountCommand(
            FirstName: _firstName,
            LastName: _lastName,
            OtherNames: _otherNames,
            Gender: _gender,
            PhoneNumber: _phoneNumber,
            DistrictId: _districtId,
            AccountType: AccountType.Generator)
        {
            RequestId = _requestId
        };
    }

    /// <summary>
    /// Creates a valid command with default test values.
    /// </summary>
    public static RegisterUserAccountCommand CreateValid()
    {
        return new RegisterUserAccountCommandBuilder().Build();
    }

    /// <summary>
    /// Creates a valid command with a specific phone number.
    /// </summary>
    public static RegisterUserAccountCommand CreateValidWithPhoneNumber(string phoneNumber)
    {
        return new RegisterUserAccountCommandBuilder()
            .WithPhoneNumber(phoneNumber)
            .Build();
    }
}
