// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.NotificationsManagment.UseCases.GetNotifications;

public record GetNotificationsQuery(
    int Page,
    int PageSize,
    bool? UnseenOnly);

public record NotificationDto(string Id, string Title, string Body, string TriggeredBy, string CreatedAt);
