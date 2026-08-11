// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.CheckIfUserIsVerified;

public class CheckIfUserIsVerifiedQueryHandler(IUserManagementDbContext dbContext)
{
    public async Task<bool> Handle(CheckIfUserIsVerifiedQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await dbContext
            .ApplicationUsers
            .AsNoTracking()
            .Where(a => a.UserName == query.UserId && a.PhoneNumberConfirmed)
            .AnyAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
