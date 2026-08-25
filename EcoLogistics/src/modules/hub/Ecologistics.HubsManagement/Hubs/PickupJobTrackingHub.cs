// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Hubs;

public class PickupJobTrackingHub : Hub
{
    private readonly IPickupJobAuthorizationService _authService;
    private readonly ITrackingStore _trackingStore;

    public PickupJobTrackingHub(IPickupJobAuthorizationService authService, ITrackingStore trackingStore)
    {
        _authService = authService;
        _trackingStore = trackingStore;
    }

    public async Task JoinPickupJobTracking(Guid pickupJobId)
    {
        string? userId = Context.UserIdentifier;

        if (string.IsNullOrWhiteSpace(userId))
            throw new HubException("You must be logged in to track a pickup job.");

        bool isAuthorized = await _authService.CanTrackJob(pickupJobId, userId).ConfigureAwait(false);

        if (!isAuthorized)
            throw new HubException("You are not authorized to track this pickup job.");

        await Groups.AddToGroupAsync(Context.ConnectionId, GroupName(pickupJobId)).ConfigureAwait(false);
    }

    public async Task LeavePickupJobTracking(Guid pickupJobId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName(pickupJobId)).ConfigureAwait(false);
    }

    public async Task UpdateLocation(Guid pickupJobId, double latitude, double longitude)
    {
        string? userId = Context.UserIdentifier;
        if (string.IsNullOrWhiteSpace(userId))
            throw new HubException("You must be logged in to track a pickup job.");

        if(!await _authService.IsAssignedCollector(pickupJobId, userId).ConfigureAwait(false))
            throw new HubException("Not the assigned collector for this job.");

        PickupJobTrackingState state = await _trackingStore.GetOrInitAsync(pickupJobId, latitude, longitude).ConfigureAwait(false);

        double remainingMeters = HaversineDistance(latitude, longitude, state.DistinationLat, state.DestinationLon);

        double percentage = state.InitialDistanceMeters <= 0
            ? 100
            : Math.Clamp(100 * (1 - remainingMeters / state.InitialDistanceMeters), 0, 100);

        double avgSpeedMps = await _trackingStore.UpdateSamplesAndGetAvgSpeedAsync(
            pickupJobId, latitude, longitude, DateTimeOffset.UtcNow)
            .ConfigureAwait(false);

        // guard against divide-by-zero -> Infinity, same trap as before
        int? etaSeconds = avgSpeedMps > 0.3 // ~1 km/h floor, ignores GPS jitter while stationary
            ? (int)(remainingMeters / avgSpeedMps)
            : null; // client shows "calculating..." instead of a bogus ETA

        await Clients.Group(GroupName(pickupJobId)).SendAsync("LocationUpdated",
            new
            {
                PickupJobId = pickupJobId,
                Latitude = latitude,
                Longitude = longitude,
                PercentageComplete = Math.Round(percentage, 1),
                RemainingMeters = Math.Round(remainingMeters),
                EtaSeconds = etaSeconds,
                TimeStamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            }).ConfigureAwait(false);
    }

    private static string GroupName(Guid pickupJobId) => $"PickupJobTracking-{pickupJobId}";
    private static double HaversineDistance(double lat1, double lon1, double lat2, double lon2) =>
        Math.Sqrt(Math.Pow(lat2 - lat1, 2) + Math.Pow(lon2 - lon1, 2)) * 6371;
}
