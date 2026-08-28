// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Services;

public interface IPickupManagementDbContext
{
    DbSet<District> Districts { get; }
    DbSet<WasteMeasurement> WasteMeasurements { get; }
    DbSet<PickupFee> PickupFees { get; }
    DbSet<Pickup> Pickups { get; }
    DbSet<PickupForQuery> PickupsForQuerying { get; }
    DbSet<WasteKind> WasteKinds { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
