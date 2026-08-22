// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecolgistics.NotificationsManagment.Infrastructure.Data;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecologistics.NotificationsManagment.UseCases.DI;

public static class SetNotificationsManagementServices
{
    public static IServiceCollection AddNotificationsManagementServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        ConfigureFirebase(services, configuration);
        ConfigureDbContext(services, configuration);

        return services;
    }

    private static void ConfigureFirebase(IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        string? credentialPath = configuration["Firebase:CredentialsPath"];
        if (string.IsNullOrWhiteSpace(credentialPath) || !File.Exists(credentialPath))
        {
            throw new InvalidOperationException(
                $"Firebase credentials file not found at '{credentialPath}'. " +
                "Set Firebase:CredentialsPath and mount the file with -v.");
        }

        ServiceAccountCredential serviceAccountCredential = CredentialFactory.FromFile<ServiceAccountCredential>(credentialPath);
        var googleCredential = GoogleCredential.FromServiceAccountCredential(serviceAccountCredential);

        FirebaseApp.Create(new AppOptions
        {
            Credential = googleCredential
        });

        services.AddSingleton(FirebaseMessaging.DefaultInstance);
    }

    private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationsManagementDbContext, NotificationsManagementDbContext>();

        string connectionString =
            configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<NotificationsManagementDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}
