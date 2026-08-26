// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Services;

public interface IPickupJobAuthorizationService
{
    Task<bool> CanTrackJob(Guid pickupJobId, string userId);
    Task<bool> IsAssignedCollector(Guid pickupJobId, string userId);
}
