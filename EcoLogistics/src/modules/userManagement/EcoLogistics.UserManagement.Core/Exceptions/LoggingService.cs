// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Logging;

namespace EcoLogistics.UserManagement.Core.Exceptions;

public static partial class LoggingService
{
    [LoggerMessage(EventId = 1001, Level = LogLevel.Error, Message = "An error occurred: {Message}. RequestId: {RequestId}, UserId: {UserId}, TraceId: {TraceId}")]
    public static partial void LogErrorMessage(this ILogger logger, string message, Guid? requestId, string? userId, string? traceId);

    [LoggerMessage(EventId = 1002, Level = LogLevel.Information, Message = "An information message occurred: {Message}. RequestId: {RequestId}, UserId: {UserId}, TraceId: {TraceId}")]
    public static partial void LogInformationMessage(this ILogger logger, string message, Guid? requestId, string? userId, string? traceId);

    [LoggerMessage(EventId = 1003, Level = LogLevel.Warning, Message = "A warning message occurred: {Message}. RequestId: {RequestId}, UserId: {UserId}, TraceId: {TraceId}")]
    public static partial void LogWarningMessage(this ILogger logger, string message, Guid? requestId, string? userId, string? traceId);
}
