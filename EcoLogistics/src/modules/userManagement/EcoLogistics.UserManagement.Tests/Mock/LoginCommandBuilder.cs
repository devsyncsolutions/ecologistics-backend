// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcoLogistics.UserManagement.Core.Features.StartLoginProcess;

namespace EcoLogistics.UserManagement.Tests.Mock;

internal sealed class LoginCommandBuilder
{
    private string _phoneNumber = "+15551234567";
    private string _requestId = Guid.NewGuid().ToString();

    public LoginCommandBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public LoginCommandBuilder WithRequestId(string requestId)
    {
        _requestId = requestId;
        return this;
    }

    public LoginCommand Build() =>
        new(PhoneNumber: _phoneNumber)
        {
            RequestId = _requestId
        };

    public static LoginCommand CreateValid() => new LoginCommandBuilder().Build();
}
