// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetPaymentStatusByPickupId;

public class GetPaymentStatusQueryHandler(
    IPaymentManagementDbContext dbContext)
{
    public async Task<PaymentStatus> Handle(GetPaymentStatusQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        PaymentStatus paymentStatus = await dbContext.Payments
            .Where(p => p.AssociatedPickupId == query.PickupId)
            .Select(p => p.Status)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return paymentStatus;
    }
}
