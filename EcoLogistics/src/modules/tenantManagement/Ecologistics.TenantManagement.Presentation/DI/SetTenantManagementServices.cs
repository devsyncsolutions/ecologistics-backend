// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Presentation.DI;

public static class SetTenantManagementServices
{
    public static IServiceCollection AddTenantManagementServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        ConfigureDbContext(services, configuration);

        return services;
    }

    private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TenantManagementDbContextSaveChangesInterceptor>();
        services.AddScoped<ITenantManagementDbContext, TenantManagementDbContext>();

        string connectionString =
            configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<TenantManagementDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString, optionsBuilder =>
            {
                optionsBuilder
                    .EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
            })
            .AddInterceptors(serviceProvider.GetRequiredService<TenantManagementDbContextSaveChangesInterceptor>())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}
