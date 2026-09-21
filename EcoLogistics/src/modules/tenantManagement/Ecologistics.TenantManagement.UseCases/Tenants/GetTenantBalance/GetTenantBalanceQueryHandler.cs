// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.UseCases.Tenants.GetTenantBalance;

public class GetTenantBalanceQueryHandler(
    ITenantManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task<decimal> Handle(GetTenantBalanceQuery query, CancellationToken cancellationToken)
    {
        var tenant =  await dbContext.Tenants
            .Select(t => new
            {
                t.TotalBalance,
                Members = t.Members.Select(m => m.UserId)
            })
            .Where(t => t.Members.Contains(loggedInUserService.GetUserId()))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return tenant?.TotalBalance ?? throw new InvalidOperationException("Tenant not found");
    }
}
