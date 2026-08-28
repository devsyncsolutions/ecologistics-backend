// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.Host.Utilities;

public class TracedRedisDatabase
{
    private readonly IDatabase _inner;

    public TracedRedisDatabase(IDatabase inner) => _inner = inner;

    public async Task<RedisValue> StringGetAsync(RedisKey key)
    {
        using var activity = TelemetryConfigs.Source.StartActivity("redis.string_get", ActivityKind.Client);
        activity?.SetTag("db.system", "redis");
        activity?.SetTag("db.operation", "GET");
        activity?.SetTag("cache.key", key.ToString());

        var result = await _inner.StringGetAsync(key).ConfigureAwait(false);

        activity?.SetTag("cache.hit", result.HasValue);
        if (result.HasValue)
            activity?.SetTag("cache.value_preview", Truncate(result.ToString(), 100));

        return result;
    }

    public async Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null)
    {
        using var activity = TelemetryConfigs.Source.StartActivity("redis.string_set", ActivityKind.Client);
        activity?.SetTag("db.system", "redis");
        activity?.SetTag("db.operation", "SET");
        activity?.SetTag("cache.key", key.ToString());
        activity?.SetTag("cache.value_preview", Truncate(value.ToString(), 100));

        try
        {
            return await _inner.StringSetAsync(key, value, (Expiration)expiry!).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.AddException(ex);
            throw;
        }
    }

    private static string Truncate(string s, int max) =>
        s.Length <= max ? s : s[..max] + "...";
}
