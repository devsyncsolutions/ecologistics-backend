// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Utilities;

public static class CacheKeys
{
    public static string GetRequestKey<TCommand>(string requestId) => $"idempotency:{typeof(TCommand).Name}:{requestId}";
    public static string GetUserDetailsKey(string userId) => $"UserDetails:{userId}";
    public static string GetPickupJobsKey(string geoKey) => $"PickupJobs:{geoKey}";

    private const string Alphabet = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";
    public static string GenerateARegistrationNumber(int count)
    {
        Span<byte> randomBytes = stackalloc byte[count];
        RandomNumberGenerator.Fill(randomBytes);

        Span<char> result = stackalloc char[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = Alphabet[randomBytes[i] % Alphabet.Length];
        }

        return new string(result);
    }

    public static string GetPickupFeesKey(string districtId) => $"PickupFees:{districtId}";

    public static string GetNotificationsKey(string userId, bool? queryUnseenOnly) => $"Notifications:{userId}:{queryUnseenOnly}";

    public static Task ToTask(ValueTask valueTask) => valueTask.AsTask();

    public static string CanTrackJobKey(Guid queryPickupJobId, string queryUserId) => $"CanTrackJob:{queryPickupJobId}:{queryUserId}";

    public static string IsAssignedToJobKey(Guid queryPickupJobId, string queryUserId) => $"IsAssignedToJob:{queryPickupJobId}:{queryUserId}";

    public static string StateKey(Guid jobId) => $"PickupJobTrackingState:{jobId}";
}
