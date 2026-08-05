// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Models;

public static class CacheKeys
{
    public static string GetRequestKey(string requestId) => $"Request:{requestId}";
    public static string GetUserDetailsKey(string userId) => $"UserDetails:{userId}";
}
