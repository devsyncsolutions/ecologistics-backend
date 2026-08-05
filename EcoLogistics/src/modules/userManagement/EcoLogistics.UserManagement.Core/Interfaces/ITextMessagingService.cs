// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Interfaces;

public interface ITextMessagingService
{
    Task<bool> SendAsync(
        string phoneNumber,
        string message,
        string? requestId = null,
        CancellationToken cancellationToken = default);
}
