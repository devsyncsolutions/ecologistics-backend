// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetWasteKinds;

public class GetWasteKindsQueryHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache)
{
    public async Task<PagedResponse<WasteKindDto>> Handle(GetWasteKindsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await cache.GetOrSetAsync(
            CacheKeys.GetWasteKinds,
            async _ => await FetchWasteKindsAsync(query, dbContext, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromDays(1),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromDays(2)
            },
            cancellationToken).ConfigureAwait(false);
    }

    private static async Task<PagedResponse<WasteKindDto>> FetchWasteKindsAsync(
        GetWasteKindsQuery query,
        IPickupManagementDbContext dbContext,
        CancellationToken cancellationToken)
    {
        IQueryable<WasteKind> innerQuery = dbContext.WasteKinds.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
#pragma warning disable CA1862
            innerQuery = innerQuery.Where(w => w.Name.ToLower().Contains(query.SearchTerm.ToLower()));
#pragma warning restore CA1862

        int totalCount = await innerQuery.CountAsync(cancellationToken).ConfigureAwait(false);

        int pageSize = query.PageSize <= 50 ? query.PageSize : 50;
        List<WasteKindDto> pagedResult = await innerQuery
            .OrderBy(w => w.CreatedAt)
            .ThenBy(w => w.Name)
            .Skip((query.Page - 1) * pageSize)
            .Take(pageSize)
            .Select(w => new WasteKindDto(w.Id.Value.ToString(), w.Name))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedResponse<WasteKindDto>
        {
            Items = pagedResult,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
