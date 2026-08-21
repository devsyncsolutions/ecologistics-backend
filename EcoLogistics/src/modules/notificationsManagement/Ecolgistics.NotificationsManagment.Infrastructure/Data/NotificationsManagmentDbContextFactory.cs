// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecolgistics.NotificationsManagment.Infrastructure.Data;

public sealed class NotificationsManagmentDbContextFactory : IDesignTimeDbContextFactory<NotificationsManagementDbContext>
{
    public NotificationsManagementDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var optionBuilder = new DbContextOptionsBuilder<NotificationsManagementDbContext>();

        string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                  throw new InvalidOperationException("DefaultConnection not found in configuration");

        optionBuilder.UseNpgsql(connectionString);
        return new NotificationsManagementDbContext(optionBuilder.Options);
    }
}
