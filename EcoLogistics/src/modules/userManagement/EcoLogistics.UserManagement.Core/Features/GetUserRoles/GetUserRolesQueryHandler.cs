// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.GetUserRoles;

public class GetUserRolesQueryHandler(
    IAuthenticationServices authenticationServices,
    ILoggedInUserService loggedInUserService)
{
    public async Task<UserRolesResponse> Handle(GetUserRolesQuery query, CancellationToken cancellationToken)
    {
        var roles = await authenticationServices
            .GetUserRolesAsync(loggedInUserService.GetUserId(), cancellationToken)
            .ConfigureAwait(false);

        return new UserRolesResponse(roles);
    }
}
