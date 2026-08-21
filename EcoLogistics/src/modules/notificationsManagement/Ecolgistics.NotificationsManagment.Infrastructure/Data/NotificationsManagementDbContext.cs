// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecolgistics.NotificationsManagment.Infrastructure.Data;

public class NotificationsManagementDbContext : DbContext, INotificationsManagementDbContext
{
    public NotificationsManagementDbContext(DbContextOptions<NotificationsManagementDbContext> options) : base(options)
    {
    }

    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<DeviceToken> DeviceTokens => Set<DeviceToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsManagementDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
