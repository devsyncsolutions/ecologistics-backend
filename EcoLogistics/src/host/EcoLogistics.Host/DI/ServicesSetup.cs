// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.PaymentManagement.Core.Features.GetPaymentMethods;
using Serilog;
using Serilog.Events;

namespace EcoLogistics.Host.DI;

internal static class ServicesSetup
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddUserManagementServices(builder.Configuration);
        builder.Services.AddTenantManagementServices(builder.Configuration);
        builder.Services.AddPickupManagementServices(builder.Configuration);
        builder.Services.AddNotificationsManagementServices(builder.Configuration);
        builder.Services.AddHubsManagementServices();
        builder.Services.AddPaymentManagementServices(builder.Configuration);

        ConfigureOpenApiDocumentation(builder);

#pragma warning disable S125 // Sections of code should not be commented out
        //ConfigureOpenTelemetry(builder);
        ConfigureArkesselMessagingClient(builder);
#pragma warning restore S125 // Sections of code should not be commented out
        ConfigureUtilities(builder);
        ConfigureWolverine(builder);
        ConfigureFusionCache(builder);
        ConfigureCors(builder);
        ConfigureRateLimiting(builder);
        ConfigureCarterEndpoints(builder);
        ConfigureHealthChecks(builder);
        ConfigureTickerQ(builder);
        ConfigureCloudinary(builder);
        ConfigureSignalR(builder);
        ConfigureLogging(builder);

        return builder.Build();
    }

    private static void ConfigureSignalR(WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR()
        .AddStackExchangeRedis(builder.Configuration.GetConnectionString("RedisConnection")!);
    }
    private static void ConfigureCloudinary(WebApplicationBuilder builder)
    {
        Account account = new(
            builder.Configuration["Cloudinary:CloudName"],
            builder.Configuration["Cloudinary:ApiKey"],
            builder.Configuration["Cloudinary:ApiSecret"]);

        Cloudinary cloudinary = new(account)
        {
            Api = { Secure = true }
        };

        builder.Services.AddSingleton(cloudinary);
        builder.Services.AddSingleton<IProcessFilesService, ProcessFilesService>();
    }
    private static void ConfigureTickerQ(WebApplicationBuilder builder)
    {
        builder.Services.AddTickerQ(options =>
        {
            options.AddDashboard(opt =>
            {
                opt.SetBasePath("/jobs");
                opt.WithBasicAuth(builder.Configuration["TickerQ:Dashboard:Username"]!, builder.Configuration["TickerQ:Dashboard:Password"]!);
            });

            options.AddStackExchangeRedis(redis =>
            {
                redis.Configuration = builder.Configuration.GetConnectionString("RedisConnection")!;
                redis.InstanceName = "EcoLogisticsCache";
            });

            options.AddOperationalStore(efOpt =>
            {
                efOpt.UseTickerQDbContext<TickerQDbContext>(o =>
                    o.UseNpgsql(
                        builder.Configuration.GetConnectionString("DefaultConnection")!,
                        b => b.MigrationsAssembly(typeof(ServicesSetup).Assembly.GetName().Name)));
            });
        });
    }
    private static void ConfigureHealthChecks(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<UserManagementDbContext>("UserManagementDbContext", HealthStatus.Unhealthy)
            .AddDbContextCheck<TenantManagementDbContext>("TenantManagementDbContext", HealthStatus.Unhealthy)
            .AddDbContextCheck<PickupManagementDbContext>("PickupManagementDbContext", HealthStatus.Unhealthy)
            .AddDbContextCheck<NotificationsManagementDbContext>("NotificationsManagementDbContext", HealthStatus.Unhealthy)
            .AddDbContextCheck<TickerQDbContext>("TickerQDbContext", HealthStatus.Unhealthy)
            .AddDbContextCheck<PaymentManagementDbContext>("PaymentManagementDbContext", HealthStatus.Unhealthy)
            .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!)
            .AddRabbitMQ(name: "RabbitMQ", failureStatus: HealthStatus.Unhealthy)
            .AddRedis(builder.Configuration.GetConnectionString("RedisConnection")!, name: "Redis",
                failureStatus: HealthStatus.Unhealthy);

        string rabbitConnectionString = builder.Configuration.GetConnectionString("RabbitMQConnection")!;

        builder.Services.AddSingleton<IConnection>(_ =>
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitConnectionString),
                AutomaticRecoveryEnabled = true
            };
            return factory.CreateConnectionAsync().GetAwaiter().GetResult();
        });
    }
    private static void ConfigureOpenApiDocumentation(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info.Title = "EcoLogistics API";
                document.Info.Version = "v1";
                document.Info.Description = "EcoLogistics API";
                document.Info.Description = """
                                            EcoLogistics API

                                            ## Real-time pickup tracking

                                            `PickupJobTrackingHub` is a SignalR hub available at
                                            `/hubs/v1/pickup-job-tracking`. Connect using the SignalR protocol
                                            and provide the same authentication required by the API.

                                            ### Client-to-server methods

                                            | Method | Parameters | Description |
                                            | --- | --- | --- |
                                            | `JoinPickupJobTracking` | `pickupJobId: UUID` | Adds a generator to the pickup-job tracking group. |
                                            | `LeavePickupJobTracking` | `pickupJobId: UUID` | Removes the connection from the pickup-job tracking group. |
                                            | `UpdateLocation` | `pickupJobId: UUID`, `latitude: number`, `longitude: number` | Publishes the collector's current location to the tracking group. |

                                            `JoinPickupJobTracking` is restricted to generators. `UpdateLocation`
                                            is restricted to the collector assigned to the pickup job.

                                            ### Server-to-client events

                                            #### `LocationUpdated`

                                            Sent to members of the pickup-job tracking group after a successful
                                            `UpdateLocation` call.

                                            ```json
                                            {
                                              "pickupJobId": "00000000-0000-0000-0000-000000000000",
                                              "latitude": 5.6037,
                                              "longitude": -0.1870,
                                              "percentageComplete": 42.5,
                                              "remainingMeters": 1250,
                                              "etaSeconds": 180,
                                              "timeStamp": "2026-09-21 22:42:35"
                                            }
                                            ```

                                            `etaSeconds` is `null` when the current average speed is too low
                                            to produce a reliable estimate.
                                            """;

                return Task.CompletedTask;
            });
        });
    }
    private static void ConfigureCarterEndpoints(WebApplicationBuilder builder) =>
        builder.Services.AddCarter(null, conf =>
        {
            builder.Services.AddEndpointsApiExplorer();

            var modules = typeof(ServicesSetup).Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(ICarterModule)) && t is { IsAbstract: false, IsInterface: false })
                .ToList();

            var withModuleMethod = typeof(CarterConfigurator).GetMethod(nameof(CarterConfigurator.WithModule))!;
            foreach (var module in modules)
                withModuleMethod.MakeGenericMethod(module).Invoke(conf, null);

            conf.WithResponseNegotiator<XmlResponseNegotiator>();
        });
    private static void ConfigureArkesselMessagingClient(WebApplicationBuilder builder)
    {
        string baseUrl = builder.Configuration["Arkessel:BaseUrl"]
                         ?? throw new InvalidOperationException("Arkessel:BaseUrl is not set");
        string apiKey = builder.Configuration["Arkessel:ApiKey"]
                        ?? throw new InvalidOperationException("Arkessel:ApiKey is not set");

        builder.Services.AddHttpClient("arkessel", client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("api-key", apiKey);
            })
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.Delay = TimeSpan.FromSeconds(1);
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.UseJitter = true;

                options.Retry.ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .Handle<TimeoutRejectedException>()
                    .HandleResult(result => result.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout);

                options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(60);
                options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(10);
            });

        builder.Services.Configure<Arkessel>(builder.Configuration.GetSection("Arkessel"));
        builder.Services.AddTransient(sp => sp.GetRequiredService<IOptions<Arkessel>>().Value);
    }
    private static void ConfigureUtilities(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ITextMessagingService, TextMessagingService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddExceptionHandler<GlobalExceptionMiddleware>();
        builder.Services.AddProblemDetails();
        builder.Services.AddHttpContextAccessor();

        builder.Host.UseDefaultServiceProvider((_, options) =>
        {
            options.ValidateScopes = true;
            options.ValidateOnBuild = true;
        });
    }
    private static void ConfigureWolverine(WebApplicationBuilder builder)
    {
        builder.Host.UseWolverine(options =>
        {
            options.Policies.AddMiddleware<PerformanceMiddleware>(
                chain => chain.MessageType.Name.Contains("Query", StringComparison.OrdinalIgnoreCase));

            options.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMQConnection")!)
                .AutoProvision()
                .UseConventionalRouting();

            options.PersistMessagesWithPostgresql(builder.Configuration.GetConnectionString("DefaultConnection")!);

            // Enable EF Core transaction integration
            options.UseEntityFrameworkCoreTransactions();

            // Turn on the transactional outbox and inbox patterns globally
            options.Policies.UseDurableOutboxOnAllSendingEndpoints();
            options.Policies.UseDurableInboxOnAllListeners();

            options.CodeGeneration.AlwaysUseServiceLocationFor<IAuthenticationServices>();
            options.CodeGeneration.AlwaysUseServiceLocationFor<ITextMessagingService>();
            options.CodeGeneration.AlwaysUseServiceLocationFor<IUserManagementDbContext>();
            options.CodeGeneration.AlwaysUseServiceLocationFor<ITenantManagementDbContext>();
            options.CodeGeneration.AlwaysUseServiceLocationFor<IPickupManagementDbContext>();
            options.CodeGeneration.AlwaysUseServiceLocationFor<Ecologistics.PickupManagement.Domain.Interfaces.IPickupServices>();
            options.CodeGeneration.AlwaysUseServiceLocationFor<INotificationsManagementDbContext>();
            options.CodeGeneration.AlwaysUseServiceLocationFor<IPaymentManagementDbContext>();
            options.Discovery.IncludeAssembly(typeof(RegisterUserAccountCommandHandler).Assembly);
            options.Discovery.IncludeAssembly(typeof(CreateATenantCompanyCommandHandler).Assembly);
            options.Discovery.IncludeAssembly(typeof(GetPickupJobsQueryHandler).Assembly);
            options.Discovery.IncludeAssembly(typeof(GetNotificationsQueryHandler).Assembly);
            options.Discovery.IncludeAssembly(typeof(GetDistrictBalanceQueryHandler).Assembly);
            options.Discovery.IncludeAssembly(typeof(GetPaymentMethodsQueryHandler).Assembly);
        });
    }
    private static void ConfigureFusionCache(WebApplicationBuilder builder)
    {
        var fusion = builder.Services.AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(5),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            });

        string redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection")
            ?? throw new InvalidOperationException("RedisConnection not found in configuration");

        builder.Services.AddStackExchangeRedisCache(redis =>
        {
            redis.Configuration = redisConnectionString;
            redis.InstanceName = "EcoLogisticsCache";
        });

        fusion
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .WithRegisteredDistributedCache()
            .WithBackplane(new RedisBackplane(new RedisBackplaneOptions
            {
                Configuration = redisConnectionString
            }));

        var jsonOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        fusion.WithSerializer(new FusionCacheSystemTextJsonSerializer(jsonOptions));
    }
    private static void ConfigureCors(WebApplicationBuilder builder)
    {
        string[] allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("SecurePolicy", policy =>
            {
                policy.SetIsOriginAllowed(origin =>
                    {
                        if (string.IsNullOrWhiteSpace(origin)) return false;

                        if (builder.Environment.IsDevelopment())
                            return origin.Equals("http://localhost:3000", StringComparison.OrdinalIgnoreCase);

                        return allowedOrigins.Any(allowed =>
                            origin.Equals(allowed, StringComparison.OrdinalIgnoreCase));
                    })
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                    .WithHeaders("Content-Type", "Authorization", "X-Requested-Id")
                    .AllowCredentials();
            });
        });
    }
    private static void ConfigureRateLimiting(WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    GetPartitionKey(context),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 60,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    }));


            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter =
                            ((int)retryAfter.TotalSeconds).ToString(CultureInfo.InvariantCulture);

                    await context.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails
                    {
                        Title = "Too Many Requests",
                        Status = StatusCodes.Status429TooManyRequests,
                        Detail = $"You have exceeded the rate limit. Please try again after {retryAfter.TotalSeconds} seconds."
                    }, cancellationToken: token).ConfigureAwait(false);
                }
                else
                {
                    await context.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails
                    {
                        Title = "Too Many Requests",
                        Status = StatusCodes.Status429TooManyRequests,
                        Detail = "You have exceeded the rate limit. Please try again later."
                    }, cancellationToken: token).ConfigureAwait(false);
                }
            };
        });
    }
#pragma warning disable S1144 // Unused private types or members should be removed
    private static void ConfigureOpenTelemetry(WebApplicationBuilder builder)
#pragma warning restore S1144 // Unused private types or members should be removed
    {
        string serviceName = builder.Configuration["OpenTelemetry:ServiceName"] ?? "EcoLogistics.Api";
        string serviceVersion = builder.Configuration["OpenTelemetry:ServiceVersion"] ?? "1.0.0";
        string apiKey = builder.Configuration["OpenTelemetry:ApiKey"]!;
#pragma warning disable S5332
        string otlpEndpoint = builder.Configuration["OpenTelemetry:OtelEndpoint"] ?? "http://seq:5341";
#pragma warning restore S5332

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService(serviceName, serviceVersion))
            .WithTracing(tracing => tracing
                .AddSource(TelemetryConfigs.WolverineSourceName)
                .AddSource(TelemetryConfigs.FusionSourceName)
                .AddSource(TelemetryConfigs.RedisSourceName)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                .AddEntityFrameworkCoreInstrumentation()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri($"{otlpEndpoint}/v1/traces");
                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                    o.Headers = apiKey;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri($"{otlpEndpoint}/v1/metrics");
                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                    o.Headers = apiKey;
                }));

        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion));
            logging.IncludeScopes = true;
            logging.IncludeFormattedMessage = true;
            logging.AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri($"{otlpEndpoint}/v1/logs");
                o.Protocol = OtlpExportProtocol.HttpProtobuf;
                o.Headers = apiKey;
            });
        });

        builder.Services.AddSingleton<TracedRedisDatabase>(sp =>
        {
            IConnectionMultiplexer mux = sp.GetRequiredService<IConnectionMultiplexer>();
            return new TracedRedisDatabase(mux.GetDatabase());
        });
    }
    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateLogger();

        if (builder.Environment.IsDevelopment())
        {
            builder.Host.UseSerilog((_, _, configuration) =>
            {
                configuration
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("TickerQ", LogEventLevel.Error)
                    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture);
            });
        }
        else
        {
            builder.Host.UseSerilog((_, _, configuration) =>
            {
                configuration
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("TickerQ", LogEventLevel.Error)
                    .MinimumLevel.Override("ZiggyCreatures.Caching.Fusion", LogEventLevel.Warning)
                    .MinimumLevel.Override("Wolverine", LogEventLevel.Warning)
                    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture);

                // Only configure Sentry if DSN is provided
                string? sentryDsn = builder.Configuration["Sentry:Dsn"];
                if (!string.IsNullOrEmpty(sentryDsn))
                {
                    configuration.WriteTo.Sentry(options =>
                    {
                        options.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                        options.MinimumEventLevel = LogEventLevel.Warning;
                        options.Dsn = sentryDsn;
                        options.TracesSampleRate = 0.1;
                        options.EnableLogs = true;
                    });
                }
            });
        }
    }
    private static string GetPartitionKey(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated != true)
            return $"ip:{context.Connection.RemoteIpAddress?.ToString() ?? "unknown"}";

        string userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        return !string.IsNullOrEmpty(userId)
            ? $"user:{userId}"
            : $"ip:{context.Connection.RemoteIpAddress?.ToString() ?? "unknown"}";
    }
}
