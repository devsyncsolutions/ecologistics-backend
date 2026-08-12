// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupFees;

public class GetPickupFeesQueryHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache,
    ILoggedInUserService loggedInUserService)
{
    public async Task<IReadOnlyList<GetPickupFeesResponse>> Handle(GetPickupFeesQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        string? rawDistrictId = loggedInUserService.GetDistrictId();

        if (!Guid.TryParse(rawDistrictId, out var districtGuid))
        {
            throw new BadHttpRequestException(
                "Your account has no district assigned. Please contact support.",
                StatusCodes.Status400BadRequest);
        }

        var districtId = new DistrictId(districtGuid);

        IReadOnlyList<GetPickupFeesResponse> result = await cache.GetOrSetAsync(
            CacheKeys.GetPickupFeesKey(loggedInUserService.GetDistrictId()),
            async _ => await FetchPickupFeesAsync(dbContext, districtId, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromDays(1),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromDays(1)
            }, cancellationToken).ConfigureAwait(false);

        return result;
    }

    private static async Task<IReadOnlyList<GetPickupFeesResponse>> FetchPickupFeesAsync(IPickupManagementDbContext dbContext,
        DistrictId districtId,
        CancellationToken cancellationToken)
    {
        var query = await dbContext
            .PickupFees
            .Where(pf => pf.DistrictId == districtId)
            .Select(pf => new
            {
                pf.Id,
                pf.District!.Name,
                pf.Amount,
                pf.Commission,
                Mesurement = new
                {
                    pf.WasteMeasurement!.Name,
                    pf.WasteMeasurement.UnitOfMeasurement,
                    pf.WasteMeasurement.Description,
                    pf.WasteMeasurement.WeightRange.Max,
                    pf.WasteMeasurement.WeightRange.Min
                }
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return query.Select(q => new GetPickupFeesResponse(
            PickupFeeId: q.Id.Value.ToString(),
            DistrictName: q.Name,
            Measurement: new MeasurementDto(
                Name: q.Mesurement.Name,
                UnitOfMeasurement: q.Mesurement.UnitOfMeasurement,
                Description: q.Mesurement.Description,
                Min: q.Mesurement.Min,
                Max: q.Mesurement.Max),
            Amount: q.Amount,
            Commission: q.Commission)).ToList();
    }
}
