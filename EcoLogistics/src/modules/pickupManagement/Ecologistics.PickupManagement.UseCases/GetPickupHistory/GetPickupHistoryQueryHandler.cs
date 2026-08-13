// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace Ecologistics.PickupManagement.UseCases.GetPickupHistory;

public class GetPickupHistoryQueryHandler(IPickupManagementDbContext dbContext, ILoggedInUserService loggedInUserService)
{
    public async Task<PagedResponse<PickupHistoryDto>> Handle(
        GetPickupHistoryQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var validation = new GetPickupHistoryQueryValidator();

        var validationResult = await validation
            .ValidateAsync(query, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        string userId = loggedInUserService.GetUserId();

        PickupStatus[] historyStatuses =
        [
            PickupStatus.Cancelled,
            PickupStatus.Scheduled,
            PickupStatus.Completed
        ];

        IQueryable<Pickup> pickupQuery = dbContext.Pickups
            .Where(p =>
                p.CreatedBy == userId &&
                historyStatuses.Contains(p.PickupStatus));

        if (!string.IsNullOrWhiteSpace(query.Type) &&
            Enum.TryParse<PickupStatus>(query.Type, true, out var type))
        {
            pickupQuery = pickupQuery.Where(p => p.PickupStatus == type);
        }

        if (!string.IsNullOrWhiteSpace(query.Status) &&
            Enum.TryParse<PickupStatus>(query.Status, true, out var status))
        {
            pickupQuery = pickupQuery.Where(p => p.PickupStatus == status);
        }

        int totalCount = await pickupQuery
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        int pageSize = Math.Min(query.PageSize, 10);

        List<PickupHistoryDto> pagedList = await pickupQuery
            .OrderByDescending(p => p.CreatedAt)
            .ThenBy(p => p.Type)
            .Skip((query.Page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PickupHistoryDto(
                PickupId: p.Id.Value.ToString(),
                WasteContentName: p.WasteContentName,
                Type: p.Type.ToString(),
#pragma warning disable CA1305
                CreatedAt: p.CreatedAt.ToString(),
#pragma warning restore CA1305
                Status: p.PickupStatus.ToString()))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedResponse<PickupHistoryDto>
        {
            Items = pagedList,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
