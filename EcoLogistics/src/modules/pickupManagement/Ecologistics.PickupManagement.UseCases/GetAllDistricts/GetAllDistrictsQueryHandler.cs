namespace Ecologistics.PickupManagement.UseCases.GetAllDistricts;

public class GetAllDistrictsQueryHandler(IPickupManagementDbContext dbContext)
{
    public async Task<PagedResponse<GetAllDistrictDto>> Handle(GetAllDistrictsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var innerQuery = dbContext.Districts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Region))
            innerQuery = innerQuery.Where(d => d.Region == Enum.Parse<Region>(query.Region));

        if (!string.IsNullOrWhiteSpace(query.SearchKey))
#pragma warning disable CA1862
            innerQuery = innerQuery.Where(d => d.Name.ToLower().Contains(query.SearchKey.ToLower()));
#pragma warning restore CA1862

        var list = innerQuery
            .OrderBy(d => d.Name)
            .ThenBy(d => d.Region)
            .Select(d => new GetAllDistrictDto(d.Id.Value.ToString(), d.Name, nameof(d.Region)));

        int totalCount = await list.CountAsync(cancellationToken).ConfigureAwait(false);
        int pageSize = query.PageSize <= 50 ? query.PageSize : 50;

        var pagedResult = await list
            .Skip((query.Page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedResponse<GetAllDistrictDto>
        {
            Items = pagedResult,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
