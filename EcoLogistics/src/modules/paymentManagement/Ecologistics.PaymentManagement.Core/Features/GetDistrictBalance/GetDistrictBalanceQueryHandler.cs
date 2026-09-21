// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetDistrictBalance;

public class GetDistrictBalanceQueryHandler(IPaymentManagementDbContext dbContext)
{
    public async Task<decimal> Handle(GetDistrictBalanceQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        decimal value =  await dbContext.BalanceThresholdPerDistricts
            .Where(b => b.DistrictId == request.DistrictId)
            .Select(b => b.BalanceThresholdAmount)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return value == 0
            ? throw new InvalidOperationException($"No balance threshold found for district {request.DistrictId}")
            : value;
    }
}
