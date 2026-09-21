// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.UseCases.Tenants.IsUserOwnerOfTenantAccount;

public class IsUserOwnerOfTenantAccountQueryHandler(ITenantManagementDbContext dbContext)
{
    public async Task<bool> Handle(IsUserOwnerOfTenantAccountQuery query, CancellationToken cancellationToken)
    {
        return await dbContext.Tenants
            .AnyAsync(t => t.CreatedBy == query.UserId, cancellationToken)
            .ConfigureAwait(false);
    }
}
