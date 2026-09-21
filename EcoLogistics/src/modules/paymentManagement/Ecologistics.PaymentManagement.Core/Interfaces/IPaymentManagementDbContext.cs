// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Interfaces;

public interface IPaymentManagementDbContext
{
    DbSet<BalanceThresholdPerDistrict> BalanceThresholdPerDistricts { get; }
    DbSet<UserPaymentMethod> UserPaymentMethods { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
