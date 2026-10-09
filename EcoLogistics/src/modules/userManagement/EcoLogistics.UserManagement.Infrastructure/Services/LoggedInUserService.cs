// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Infrastructure.Services;

public class LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    : ILoggedInUserService
{
    public string GetUserId() =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

    public string GetDistrictId() =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue("districtId") ?? string.Empty;

    public IReadOnlyCollection<string> Roles() =>
        httpContextAccessor
            .HttpContext?.User.FindAll(c => c.ValueType == ClaimTypes.Role).Select(c => c.Value).ToArray() ?? [];
}
