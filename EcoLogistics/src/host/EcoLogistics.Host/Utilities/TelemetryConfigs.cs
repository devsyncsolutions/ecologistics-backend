// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.Host.Utilities;

public static class TelemetryConfigs
{
    public const string RedisSourceName = "EcoLogistics.Redis";
    public const string WolverineSourceName = "Wolverine";
    public const string FusionSourceName = "ZiggyCreatures.FusionCache";
    public static readonly ActivitySource Source = new(RedisSourceName, "1.0.0");
}
