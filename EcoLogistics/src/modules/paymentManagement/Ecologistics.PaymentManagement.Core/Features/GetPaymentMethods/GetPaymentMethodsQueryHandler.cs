// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetPaymentMethods;

public class GetPaymentMethodsQueryHandler(
    IPaymentManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IMessageBus bus)
{
    public async Task<IEnumerable<UserPaymentMethodDto>> Handle(GetPaymentMethodsQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        string userId = loggedInUserService.GetUserId();
        IEnumerable<UserPaymentMethodDto> results;

        var innerQuery = dbContext.UserPaymentMethods.AsQueryable();

        if (query.AccountType == AccountType.Generator)
        {
            results = await innerQuery
                .Where(pm => pm.CreatedBy == userId)
                .Select(pm => new UserPaymentMethodDto(pm.Id.Value.ToString(), pm.Method.ToString(), pm.Status.ToString()))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        else
        {
            // check if user is the owner of the tenant account
            bool isOwnerOfTenantAccount = await bus.InvokeAsync<bool>(
                new IsUserOwnerOfTenantAccountQuery(loggedInUserService.GetUserId()),
                cancellationToken).ConfigureAwait(false);

            if(!isOwnerOfTenantAccount)
                throw new UnauthorizedAccessException("You are not authorized to view this resource.");

            results = await innerQuery
                .Where(pm => pm.CreatedBy == userId)
                .Select(pm => new UserPaymentMethodDto(pm.Id.Value.ToString(), pm.Method.ToString(), pm.Status.ToString()))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        return results;
    }
}
