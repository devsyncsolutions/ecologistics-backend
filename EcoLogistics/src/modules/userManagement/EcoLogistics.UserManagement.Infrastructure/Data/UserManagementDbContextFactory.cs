// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Infrastructure.Data;

public sealed class UserManagementDbContextFactory : IDesignTimeDbContextFactory<UserManagementDbContext>
{
    public UserManagementDbContext CreateDbContext(string[] args)
    {
        // build configuration from appSettings files
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var optionBuilder = new DbContextOptionsBuilder<UserManagementDbContext>();

        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found in configuration");

        optionBuilder.UseNpgsql(connectionString);
        return new UserManagementDbContext(optionBuilder.Options);
    }
}
