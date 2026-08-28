// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.Host.Middlewares;

public class PerformanceMiddleware
{
    private readonly Stopwatch _stopwatch = new();

    public void Before()
    {
        _stopwatch.Start();
    }

    public void Finally(
        ILoggedInUserService loggedInUserService,
        ILogger logger,
        object message,
        Envelope envelope)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(envelope);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(loggedInUserService);

        _stopwatch.Stop();
        long elapsedMs = _stopwatch.ElapsedMilliseconds;

        string userId = string.IsNullOrWhiteSpace(loggedInUserService.GetUserId())
            ? "anonymous"
            : loggedInUserService.GetUserId();

        string traceId = Activity.Current?.TraceId.ToString() ?? string.Empty;


        if (elapsedMs > 500)
        {
            logger.LogWarningMessage(
                $"Slow Query Alert: {message.GetType().Name} (ID: {envelope.Id}) took {elapsedMs}ms to execute.",
                null,
                userId,
                traceId);
        }
    }
}
