// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Commons.Utility;

public static class CreatePointCalculation
{
    private static readonly GeometryFactory s_factory =
        NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

    public static Point CreatePoint(double longitude, double latitude) =>
        s_factory.CreatePoint(new Coordinate(longitude, latitude));

    // Precision 7 ≈ 76m × 76m cells — good middle ground for a "nearby jobs" feed.
    // Precision 6 ≈ 610m × 610m if you want coarser buckets and higher hit rates.
    private const int GeohashPrecision = 7;

    public static string BuildKey(double latitude, double longitude, double radiusMeters)
    {
        string cell = NGeoHash.GeoHash.Encode(latitude, longitude, GeohashPrecision);
        double snappedRadius = SnapRadius(radiusMeters);

        return $"available-pickups:{cell}:r{snappedRadius}";
    }

    // Snap arbitrary client-supplied radii to a fixed set of buckets,
    // so radius=487 and radius=502 share a cache entry instead of fragmenting the cache.
    private static double SnapRadius(double radiusMeters)
    {
        double[] buckets = [500, 1000, 2000, 5000, 10000, 50000];
        return buckets.FirstOrDefault(b => radiusMeters <= b, buckets[^1]);
    }
}
