// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Services;

public interface ITrackingStore
{
    Task<PickupJobTrackingState> GetOrInitAsync(Guid jobId, double lat, double lng);
    Task<double> UpdateSamplesAndGetAvgSpeedAsync(
        Guid pickupJobId,
        double latitude,
        double longitude,
        DateTimeOffset timestamp);
}
