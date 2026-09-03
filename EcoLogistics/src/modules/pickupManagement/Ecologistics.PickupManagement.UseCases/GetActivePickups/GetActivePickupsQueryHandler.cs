// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetActivePickups;

public class GetActivePickupsQueryHandler(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task<IReadOnlyList<ActivePickupDto>> Handle(GetActivePickupsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        string userId = loggedInUserService.GetUserId();

        PickupStatus[] activeStatuses =
        [
            PickupStatus.Started,
            PickupStatus.Matched,
            PickupStatus.InProgress,
            PickupStatus.CollectorEnRoute
        ];

        List<ActivePickupDto> activePickupJobs = await dbContext
            .Pickups
            .Where(p =>
                p.CreatedBy == userId &&
                activeStatuses.Contains(p.PickupStatus))
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ActivePickupDto(
                PickupId: p.Id.Value.ToString(),
                ContentName: p.WasteContentName,
#pragma warning disable CA1305
                StartedAt: p.TimeStamp.JobStartedAt.ToString()!,
                Status: p.PickupStatus.ToString(),
                Reference: p.Reference))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
#pragma warning restore CA1305

        return activePickupJobs;
    }
}
