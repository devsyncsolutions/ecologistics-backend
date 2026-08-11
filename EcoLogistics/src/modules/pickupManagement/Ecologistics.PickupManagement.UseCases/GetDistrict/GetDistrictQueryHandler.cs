namespace Ecologistics.PickupManagement.UseCases.GetDistrict;

public class GetDistrictQueryHandler(IPickupManagementDbContext dbContext)
{
    public async Task<GetDistrictResponse?> Handle(GetDistrictQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await dbContext
        .Districts
        .Where(d => d.Id == new DistrictId(Guid.Parse(query.DistrictId)))
        .Select(d => new GetDistrictResponse(d.Id.ToString(), d.Name))
        .FirstOrDefaultAsync(cancellationToken)
        .ConfigureAwait(false);
    }
}