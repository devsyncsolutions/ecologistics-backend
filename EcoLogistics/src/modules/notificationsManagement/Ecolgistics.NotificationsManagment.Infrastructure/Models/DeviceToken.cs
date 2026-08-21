// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Enums;

namespace Ecolgistics.NotificationsManagment.Infrastructure.Models;

public class DeviceToken
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
    public DevicePlatform Platform { get; set; }
    public DateTimeOffset RegisteredAt { get; set; }
}
