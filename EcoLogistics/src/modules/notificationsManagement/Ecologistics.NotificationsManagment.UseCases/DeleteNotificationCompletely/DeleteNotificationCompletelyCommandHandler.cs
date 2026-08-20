// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.NotificationsManagment.UseCases.DeleteNotificationCompletely;

public class DeleteNotificationCompletelyCommandHandler(
    INotificationsManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IFusionCache cache)
{
    public async Task Handle(DeleteNotificationCompletelyCommand command, CancellationToken cancellationToken)
    {
        string userId = loggedInUserService.GetUserId();

        int affectedRows = await dbContext
            .Notifications
            .Where(n =>
                n.Id == command.NotificationId &&
                n.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken)
            .ConfigureAwait(false);

        if (affectedRows == 0)
            throw new NotFoundException("Notification not found.");

        Task clearCacheWhenNegative = CacheKeys.ToTask(cache.RemoveAsync(
            CacheKeys.GetNotificationsKey(userId, false),
            token: cancellationToken));

        Task clearCacheWhenPositive = CacheKeys.ToTask(cache.RemoveAsync(
            CacheKeys.GetNotificationsKey(userId, true),
            token: cancellationToken));

        await Task.WhenAll(clearCacheWhenNegative, clearCacheWhenPositive).ConfigureAwait(false);
    }
}
