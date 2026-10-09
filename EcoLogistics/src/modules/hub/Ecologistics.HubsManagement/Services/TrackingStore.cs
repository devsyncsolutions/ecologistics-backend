// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Services;

public class TrackingStore(
    IFusionCache cache, IMessageBus bus) : ITrackingStore
{
    private const int MaxSamples = 10;
    private static readonly TimeSpan s_trackingTtl = TimeSpan.FromHours(4);

    /// <summary>
    /// Gets the tracking state for the given job id. If it doesn't exist, it will be created.'
    /// </summary>
    /// <param name="jobId">Guid</param>
    /// <param name="lat">double</param>
    /// <param name="lng">double</param>
    /// <returns>PickupJobTrackingState</returns>
    public async Task<PickupJobTrackingState> GetOrInitAsync(Guid jobId, double lat, double lng)
    {
        MaybeValue<PickupJobTrackingState> existing = await cache.TryGetAsync<PickupJobTrackingState>(
            CacheKeys.StateKey(jobId)).ConfigureAwait(false);
        if(existing.HasValue) return existing.Value;

        (double destLat, double destLng) = await bus.InvokeAsync<(double, double)>(new PickupDestinationLookupQuery(jobId))
            .ConfigureAwait(false);
        double initialDistance = GeoDistance.HaversineMeters(lat, lng, destLat, destLng);

        var state = new PickupJobTrackingState(destLat, destLng, initialDistance,
            [new LocationSample(lat, lng, DateTimeOffset.UtcNow)]);

        ValueTask setCacheTask =  cache.SetAsync(CacheKeys.StateKey(jobId), state, new FusionCacheEntryOptions { Duration = s_trackingTtl });
        ValueTask publishTask = bus.PublishAsync(new MovePickupJobToProgressCommand(jobId));
        await Task.WhenAll(setCacheTask.AsTask(), publishTask.AsTask()).ConfigureAwait(false);

        return state;
    }

    /// <summary>
    /// Updates the tracking state for the given job id.
    /// </summary>
    /// <param name="pickupJobId"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="timestamp"></param>
    /// <returns>double</returns>
    public async Task<double> UpdateSamplesAndGetAvgSpeedAsync(Guid pickupJobId, double latitude, double longitude,
        DateTimeOffset timestamp)
    {
        MaybeValue<PickupJobTrackingState> existing = await cache
            .TryGetAsync<PickupJobTrackingState>(CacheKeys.StateKey(pickupJobId))
            .ConfigureAwait(false);
        if (!existing.HasValue)
        {
            // GetOrInitAsync should always run first via the hub, but guard rather than throw here —
            // a missing state shouldn't take down a location tick.
            return 0;
        }

        PickupJobTrackingState state = existing.Value;
        var samples = new Collection<LocationSample>(state.RecentSamples)
        {
            new(latitude, longitude, timestamp)
        };

        if (samples.Count > MaxSamples)
            samples.RemoveAt(0);

        PickupJobTrackingState updatedState = state with { RecentSamples = samples };
        await cache.SetAsync(CacheKeys.StateKey(pickupJobId), updatedState,
            new FusionCacheEntryOptions { Duration = s_trackingTtl })
            .ConfigureAwait(false);

        return ComputeAverageSpeedMetersPerSecond(samples);
    }

    private static double ComputeAverageSpeedMetersPerSecond(Collection<LocationSample> samples)
    {
        if (samples.Count < 2)
            return 0;

        double totalDistance = 0;
        double totalSeconds = 0;

        for (int i = 1; i < samples.Count; i++)
        {
            LocationSample prev = samples[i - 1];
            LocationSample curr = samples[i];

            double elapsedSeconds = (curr.Timestamp - prev.Timestamp).TotalSeconds;

            // Guard against clock skew or duplicate/out-of-order ticks producing a zero or negative
            // interval — same class of bug as the DaysIntervalForPickupIfRecurring division earlier,
            // just here it'd silently poison the average rather than throw at serialization time.
            if (elapsedSeconds <= 0)
                continue;

            totalDistance += GeoDistance.HaversineMeters(prev.Lat, prev.Lon, curr.Lat, curr.Lon);
            totalSeconds += elapsedSeconds;
        }

        return totalSeconds > 0 ? totalDistance / totalSeconds : 0;
    }
}
