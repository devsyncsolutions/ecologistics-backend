// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.Host.DI;

internal static class ServicesSetup
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddUserManagementServices(builder.Configuration);

        ConfigureLogging(builder);

        builder.Services.AddOpenApi();

        return builder.Build();
    }

    private static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateLogger();

        if (builder.Environment.IsDevelopment())
        {
            builder.Host.UseSerilog((_, _, configuration) =>
            {
                configuration.MinimumLevel.Information()
                    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                    .WriteTo.File(
                        "logs/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        formatProvider: CultureInfo.InvariantCulture);
            });
        }
        else
        {
            builder.Host.UseSerilog((_, _, configuration) =>
            {
                configuration
                    .MinimumLevel.Information()
                    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture);

                string sentryDsn = builder.Configuration["SENTRY_DSN"] ?? throw new InvalidOperationException("SENTRY_DSN is not set");

                if (!string.IsNullOrWhiteSpace(sentryDsn))
                {
                    configuration.WriteTo.Sentry(opt =>
                    {
                        opt.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                        opt.MinimumEventLevel = LogEventLevel.Warning;
                        opt.Dsn = sentryDsn;
                        opt.TracesSampleRate = 0.1;
                        opt.EnableLogs = true;
                    });
                }
            });
        }
    }
}
