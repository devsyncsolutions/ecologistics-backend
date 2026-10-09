// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.UseCases.Tenants.UpdateTenantBalance;

public class UpdateTenantBalanceCommandHandler(
    ITenantManagementDbContext dbContext,
    ILogger<UpdateTenantBalanceCommandHandler> logger)
{
    public async Task Handle(UpdateTenantBalanceCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        Tenant? tenant = await dbContext.Tenants
            .AsTracking()
            .Include(t => t.Members)
            .Where(t => t.Members.Any(m => m.UserId == command.CollectorMemberId))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (tenant is null)
        {
            logger.LogErrorMessage(
                $"Tenant not found for collector member with ID: {command.CollectorMemberId}",
                null,
                command.CollectorMemberId,
                null);
            throw new InvalidOperationException("Tenant not found");
        }

        tenant.UpdateTenantBalance(command.Amount);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
