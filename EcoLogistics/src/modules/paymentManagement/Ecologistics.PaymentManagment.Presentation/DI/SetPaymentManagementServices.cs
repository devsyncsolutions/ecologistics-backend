// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagment.Presentation.DI;

public static class SetPaymentManagementServices
{
    public static IServiceCollection AddPaymentManagementServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<PaystackOptions>(configuration.GetSection("PayStack"));
        ConfigureDbContext(services, configuration);
        ConfigurePayStackMessagingClient(services, configuration);

        services.AddScoped<PaymentManagementDbContextSaveChangesInterceptor>();
        services.AddScoped<IPaymentManagementDbContext, PaymentManagementDbContext>();

        return services;
    }

    private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString =
            configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<PaymentManagementDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString, optionsBuilder =>
                {
                    optionsBuilder
                        .EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                })
                .AddInterceptors(serviceProvider.GetRequiredService<PaymentManagementDbContextSaveChangesInterceptor>())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
    private static void ConfigurePayStackMessagingClient(IServiceCollection services, IConfiguration configuration)
    {
        PaystackOptions paystackOptions = configuration.GetSection("PayStack").Get<PaystackOptions>() ??
                                          throw new InvalidOperationException("PayStack configuration section is missing or invalid.");

        string baseUrl = paystackOptions.BaseUrl;
        string secretKey = paystackOptions.SecretKey;
        string sectionName = paystackOptions.SectionName;

        services.AddHttpClient<IPaystackServices, PaystackServices>(sectionName, client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
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
}
