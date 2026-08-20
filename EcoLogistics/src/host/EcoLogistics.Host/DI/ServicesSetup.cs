// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudinaryDotNet;
using Ecologistics.Common.Interfaces;
using EcoLogistics.Host.Middlewares;
using Ecologistics.NotificationsManagment.UseCases.DI;
using Ecologistics.PickupManagement.Infrastructure.Data;
using Ecologistics.PickupManagement.Infrastructure.Services;
using Ecologistics.PickupManagement.UseCases.DI;
using Ecologistics.PickupManagement.UseCases.GetPickupJobs;
using Ecologistics.TenantManagement.Infrastructure.Data;
using Ecologistics.TenantManagement.Presentation.DI;
using Ecologistics.TenantManagement.UseCases.Interfaces;
using Ecologistics.TenantManagement.UseCases.Tenants.CreateATenantCompany;
using EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;
using EcoLogistics.UserManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Polly.Timeout;
using RabbitMQ.Client;
using TickerQ.Caching.StackExchangeRedis.DependencyInjection;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DbContextFactory;
using TickerQ.EntityFrameworkCore.DependencyInjection;
using Wolverine.EntityFrameworkCore;

namespace EcoLogistics.Host.DI;

internal static class ServicesSetup
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddUserManagementServices(builder.Configuration);
        builder.Services.AddTenantManagementServices(builder.Configuration);
        builder.Services.AddPickupManagementServices(builder.Configuration);
        builder.Services.AddNotificationsManagementServices(builder.Configuration);

        ConfigureOpenApiDocumentation(builder);
        ConfigureLogging(builder);
        ConfigureArkesselMessagingClient(builder);
        ConfigureUtilities(builder);
        ConfigureWolverine(builder);
        ConfigureFusionCache(builder);
        ConfigureCors(builder);
        ConfigureRateLimiting(builder);
        ConfigureCarterEndpoints(builder);
        ConfigureHealthChecks(builder);
        ConfigureTickerQ(builder);
        ConfigureCloudinary(builder);

        builder.Services.AddTransient<ITextMessagingService, TextMessagingService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddExceptionHandler<GlobalExceptionMiddleware>();
        builder.Services.AddProblemDetails();
        builder.Services.AddHttpContextAccessor();

        builder.Host.UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes = true;
            options.ValidateOnBuild = true;
        });

        return builder.Build();
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
        builder.Services.AddSingleton<IProcessImagesService, ProcessImagesService>();
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

            if (builder.Environment.IsDevelopment()) return;

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
        var healthChecks = builder.Services.AddHealthChecks()
            .AddDbContextCheck<UserManagementDbContext>("UserManagementDbContext", HealthStatus.Unhealthy)
            .AddDbContextCheck<TenantManagementDbContext>("TenantManagementDbContext", HealthStatus.Unhealthy)
            .AddDbContextCheck<PickupManagementDbContext>("PickupManagementDbContext", HealthStatus.Unhealthy)
            .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

        if (builder.Environment.IsDevelopment())
            return;

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

        healthChecks
            .AddDbContextCheck<TickerQDbContext>("TickerQDbContext", HealthStatus.Unhealthy)
            .AddRabbitMQ(name: "RabbitMQ", failureStatus: HealthStatus.Unhealthy)
            .AddRedis(builder.Configuration.GetConnectionString("RedisConnection")!, name: "Redis",
                failureStatus: HealthStatus.Unhealthy);
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

                string sentryDsn = builder.Configuration["Sentry:Dsn"] ?? throw new InvalidOperationException("SENTRY_DSN is not set");

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
    }
    private static void ConfigureUtilities(WebApplicationBuilder builder)
    {
        builder.Services.Configure<Arkessel>(builder.Configuration.GetSection("Arkessel"));
        builder.Services.AddTransient(sp => sp.GetRequiredService<IOptions<Arkessel>>().Value);
    }
    private static void ConfigureWolverine(WebApplicationBuilder builder)
    {
        builder.Host.UseWolverine(options =>
        {
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
            options.Discovery.IncludeAssembly(typeof(RegisterUserAccountCommandHandler).Assembly);
            options.Discovery.IncludeAssembly(typeof(CreateATenantCompanyCommandHandler).Assembly);
            options.Discovery.IncludeAssembly(typeof(GetPickupJobsQueryHandler).Assembly);
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
