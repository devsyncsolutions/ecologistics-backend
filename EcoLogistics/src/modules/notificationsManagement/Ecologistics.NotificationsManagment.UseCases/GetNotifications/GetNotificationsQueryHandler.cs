// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecolgistics.NotificationsManagment.Infrastructure.Models;

namespace Ecologistics.NotificationsManagment.UseCases.GetNotifications;

public class GetNotificationsQueryHandler(
    INotificationsManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IFusionCache cache)
{
    public async Task<PagedResponse<NotificationDto>> Handle(GetNotificationsQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        string userId = loggedInUserService.GetUserId();

        PagedResponse<NotificationDto> results = await cache.GetOrSetAsync(
            CacheKeys.GetNotificationsKey(userId, query.UnseenOnly),
            async _ => await FetchNotificationsAsync(query, userId, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            }, cancellationToken).ConfigureAwait(false);

        return results;
    }

    private async Task<PagedResponse<NotificationDto>> FetchNotificationsAsync(GetNotificationsQuery query,
        string userId, CancellationToken cancellationToken)
    {
        IQueryable<Notification> notificationsQuery = dbContext
            .Notifications
            .Where(n => n.UserId == userId);

        if (query.UnseenOnly is not null)
            notificationsQuery = notificationsQuery.Where(n => n.IsSeen == query.UnseenOnly);

        int pageSize = Math.Min(query.PageSize, 10);
        IQueryable<Notification> pagedList = notificationsQuery
            .OrderBy(n => n.CreatedAt)
            .ThenBy(n => n.IsSeen)
            .Skip((query.Page - 1) * pageSize)
            .Take(pageSize);

        List<NotificationDto> finalList = await pagedList
            .Select(n => new NotificationDto(
                Id: n.Id.ToString(),
                Title: n.Title,
                Body: n.Body,
                TriggeredBy: n.TriggeredBy,
#pragma warning disable CA1305
                CreatedAt: n.CreatedAt.ToString()))
#pragma warning restore CA1305
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        int totalCount = await notificationsQuery.CountAsync(cancellationToken).ConfigureAwait(false);
        return new PagedResponse<NotificationDto>
        {
            TotalCount = totalCount,
            Items = finalList,
            Page = query.Page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
