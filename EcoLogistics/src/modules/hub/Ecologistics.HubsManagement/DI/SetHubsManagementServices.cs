// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.DI;

public static class SetHubsManagementServices
{
    public static IServiceCollection AddHubsManagementServices(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IPickupJobAuthorizationService, PickupJobAuthorizationService>();
        services.AddScoped<ITrackingStore, TrackingStore>();

        return services;
    }
}
