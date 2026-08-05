// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.EndLoginProcess;

public record GetTokenCommand(string PhoneNumber, string OtpCode) : BaseRequest;

public record GetTokenResponse(string AccessToken, string RefreshToken);
