// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace EcoLogistics.Host.Middlewares;

#pragma warning disable CA1515
public sealed class GlobalExceptionMiddleware : IExceptionHandler
#pragma warning restore CA1515
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(exception);

        ProblemDetails response;

        switch (exception)
        {
            case UnauthorizedAccessException ex:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = ex.Message
                };
                break;

            case ValidationException ex:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = ex.Errors.Any() ? string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)) : ex.Message
                };
                break;

            case BadHttpRequestException or BadRequestException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message
                };
                break;

            case NotFoundException ex:
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                response = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = ex.Message
                };
                break;

            default:
                _logger.LogErrorMessage($"Exception: {exception.Message}\nStack Trace: {exception.StackTrace}", null, null, httpContext.TraceIdentifier);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An internal server error occurred.",
                };
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken).ConfigureAwait(false);
        return true;
    }
}
