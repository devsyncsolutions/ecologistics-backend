namespace EcoLogistics.Host.Extensions;

public static class DatabaseMigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var contexts = new DbContext[]
        {
            services.GetRequiredService<UserManagementDbContext>(),
            services.GetRequiredService<TenantManagementDbContext>(),
            services.GetRequiredService<PickupManagementDbContext>(),
            services.GetRequiredService<NotificationsManagementDbContext>()
        };

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
        foreach (var context in contexts)
        {
            if (context.Database.IsRelational())
            {
                await context.Database.MigrateAsync().ConfigureAwait(false);
            }
        }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    }
}