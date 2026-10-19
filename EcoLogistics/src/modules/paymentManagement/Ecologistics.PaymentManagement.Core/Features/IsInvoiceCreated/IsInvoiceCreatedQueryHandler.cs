// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.IsInvoiceCreated;

public class IsInvoiceCreatedQueryHandler(IPaymentManagementDbContext dbContext)
{
    public async Task<bool> Handler(IsInvoiceCreatedQuery query, CancellationToken cancellationToken)
        => await dbContext.PickupJobInvoices.AnyAsync(pi => pi.AssociatedPickupId == query.PickupId, cancellationToken)
            .ConfigureAwait(false);
}
