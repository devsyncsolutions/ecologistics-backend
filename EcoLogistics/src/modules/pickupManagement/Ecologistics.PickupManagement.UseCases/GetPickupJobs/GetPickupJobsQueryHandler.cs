// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NetTopologySuite.Geometries;

namespace Ecologistics.PickupManagement.UseCases.GetPickupJobs;

public class GetPickupJobsQueryHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache)
{
    public async Task<PagedResponse<PickupJobDto>> Handle(
        GetPickupJobsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var validation = new GetPickupJobsRequestValidator();
        ValidationResult validationResult = await validation.ValidateAsync(query, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        string cacheKey = CreatePointCalculation.BuildKey(query.Latitude, query.Longitude, query.RadiusInMeters, query.Page, query.PageSize);
        PagedResponse<PickupJobDto> result = await cache.GetOrSetAsync(
            CacheKeys.GetPickupJobsKey(cacheKey),
            async _ => await FetchPickupJobsAsync(dbContext, query, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(10),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(15)
            },
            cancellationToken).ConfigureAwait(false);

        return result;
    }

    private static async Task<PagedResponse<PickupJobDto>> FetchPickupJobsAsync(
        IPickupManagementDbContext dbContext,
        GetPickupJobsQuery query,
        CancellationToken cancellationToken)
    {

        Point origin = CreatePointCalculation.CreatePoint(query.Longitude, query.Latitude);

        IQueryable<PickupForQuery> dbQuery = dbContext
            .PickupsForQuerying
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Type))
            dbQuery = dbQuery.Where(q => q.Type == query.Type);

        if(!string.IsNullOrWhiteSpace(query.Priority))
            dbQuery = dbQuery.Where(q => q.Priority == query.Priority);

        IQueryable<PickupJobDto> list = dbQuery
            .Where(p =>
                p.Coordinates.IsWithinDistance(origin, query.RadiusInMeters))
            .OrderBy(p => p.Coordinates.Distance(origin))
            .ThenBy(p => p.PickupId)
            .Select(p => new PickupJobDto(
                PickupId:p.PickupId.ToString(),
                Reference: p.Reference,
                ContentName: p.ContentName,
                FormattedAddress: p.FormattedAddress,
                Type: p.Type,
                Amount: p.Amount,
                Commission: p.Commission,
                AverageWeight: p.AverageWeight,
                CreatedAt: p.CreatedAt.ToString(CultureInfo.InvariantCulture),
                NumberOfContainers: p.NumberOfContainers));

        int totalCount = await list.CountAsync(cancellationToken).ConfigureAwait(false);

        int pageSize = query.PageSize <= 10 ? query.PageSize : 10;
        List<PickupJobDto> pagedResult = await list
            .Skip((query.Page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedResponse<PickupJobDto>
        {
            Items = pagedResult,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
