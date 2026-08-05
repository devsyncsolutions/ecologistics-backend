// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.StartLoginProcess;

public record LoginCommand(string PhoneNumber) : BaseRequest;

public record LoginResponse(string PhoneNumber);

public record SendOtpCodeCommand(string PhoneNumber, string OtpCode, string RequestId);
