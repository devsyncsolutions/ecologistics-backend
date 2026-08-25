// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Models;

public record PickupJobTrackingState(
    double DistinationLat,
    double DestinationLon,
    double InitialDistanceMeters,
    Collection<LocationSample> RecentSamples);
