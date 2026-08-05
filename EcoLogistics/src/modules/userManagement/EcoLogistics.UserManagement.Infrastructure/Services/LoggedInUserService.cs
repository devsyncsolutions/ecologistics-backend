// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Infrastructure.Services;

public class LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    : ILoggedInUserService
{
    public string GetUserId() =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new UnauthorizedAccessException("User is not logged in.");
}
