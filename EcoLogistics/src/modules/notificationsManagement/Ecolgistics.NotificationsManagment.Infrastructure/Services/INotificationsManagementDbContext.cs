// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecolgistics.NotificationsManagment.Infrastructure.Services;

public interface INotificationsManagementDbContext
{
    DbSet<Notification> Notifications { get; }
    DbSet<DeviceToken> DeviceTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
