// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.HubsManagement.Hubs;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TickerQ.DependencyInjection;

namespace EcoLogistics.Host.DI;

internal static class PipelineSetup
{
    public static WebApplication AddPipeline(this WebApplication app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedProto |
                               ForwardedHeaders.XForwardedHost
        });

        SetupScalarDocumentation(app);

        app.UseHttpsRedirection();

        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseExceptionHandler();
        app.UseCors("SecurePolicy");
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRateLimiter();

        var versionGroup = app.MapGroup("/api/v1")
            .RequireAuthorization();

        versionGroup.MapCarter();

        app.MapHub<PickupJobTrackingHub>("/hubs/v1/pickup-job-tracking")
            .RequireAuthorization();

        app.UseTickerQ();

        return app;
    }

    private static void SetupScalarDocumentation(WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.WithTitle("EcoLogistics API")
                .WithTheme(ScalarTheme.Solarized)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
    }
}
