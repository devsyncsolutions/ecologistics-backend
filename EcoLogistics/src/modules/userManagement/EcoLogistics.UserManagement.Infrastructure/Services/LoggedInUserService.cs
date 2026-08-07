// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Interfaces;

namespace EcoLogistics.UserManagement.Infrastructure.Services;

public class LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    : ILoggedInUserService
{
    public string GetUserId() =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
