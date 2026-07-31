// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.Host.DI;

internal static class ServicesSetup
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddUserManagementServices(builder.Configuration);

        builder.Services.AddOpenApi();

        return builder.Build();
    }
}
