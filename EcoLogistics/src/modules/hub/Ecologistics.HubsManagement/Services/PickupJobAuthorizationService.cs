// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Services;

public class PickupJobAuthorizationService(
    IMessageBus bus) : IPickupJobAuthorizationService
{
    public async Task<bool> CanTrackJob(Guid pickupJobId, string userId)
        => await bus.InvokeAsync<bool>(new GeneratorCanTrackPickupJobQuery(pickupJobId, userId), CancellationToken.None)
        .ConfigureAwait(false);

    public async Task<bool> IsAssignedCollector(Guid pickupJobId, string userId)
        => await bus.InvokeAsync<bool>(new CollectorIsAssignedToPickupJobQuery(pickupJobId, userId),
            CancellationToken.None)
        .ConfigureAwait(false);
}
