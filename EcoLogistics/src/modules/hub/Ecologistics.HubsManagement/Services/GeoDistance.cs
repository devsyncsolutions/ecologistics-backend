// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Services;

internal static class GeoDistance
{
    private const double EarthRadiusMeters = 6371000;

    public static double HaversineMeters(double latitude1, double longitude1, double latitude2, double longitude2)
    {
        double latitudeDelta = ToRadians(latitude2 - latitude1);
        double longitudeDelta = ToRadians(longitude2 - longitude1);

        double haversine = Math.Sin(latitudeDelta / 2) * Math.Sin(latitudeDelta / 2) +
                           Math.Cos(ToRadians(latitude1)) * Math.Cos(ToRadians(latitude2)) *
                           Math.Sin(longitudeDelta / 2) * Math.Sin(longitudeDelta / 2);

        double centralAngle = 2 * Math.Atan2(Math.Sqrt(haversine), Math.Sqrt(1 - haversine));
        return EarthRadiusMeters * centralAngle;
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180;
}
