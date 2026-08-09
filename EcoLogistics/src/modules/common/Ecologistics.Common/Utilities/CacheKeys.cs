// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Utilities;

public static class CacheKeys
{
    public static string GetRequestKey<TCommand>(string requestId) => $"idempotency:{typeof(TCommand).Name}:{requestId}";
    public static string GetUserDetailsKey(string userId) => $"UserDetails:{userId}";
    public static string GetPickupJobsKey(string geoKey) => $"PickupJobs:{geoKey}";
}
