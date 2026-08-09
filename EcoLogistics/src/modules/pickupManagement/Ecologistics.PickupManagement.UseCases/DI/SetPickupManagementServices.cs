// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.DI;

public static class SetPickupManagementServices
{
    public static IServiceCollection AddPickupManagementServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        ConfigureDbContext(services, configuration);

        return services;
    }

    private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPickupManagementDbContext, PickupManagementDbContext>();
        services.AddScoped<PickupManagementDbContextSaveChangesInterceptor>();
        services.AddScoped<IPickupServices, PickupServices>();

        string connectionString =
            configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<PickupManagementDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString, optionsBuilder =>
                {
                    optionsBuilder.UseNetTopologySuite();
                    optionsBuilder
                        .EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                })
                .AddInterceptors(serviceProvider.GetRequiredService<PickupManagementDbContextSaveChangesInterceptor>())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}
