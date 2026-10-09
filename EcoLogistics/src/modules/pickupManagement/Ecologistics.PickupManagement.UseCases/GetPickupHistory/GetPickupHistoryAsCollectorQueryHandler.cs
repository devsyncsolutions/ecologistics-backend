// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupHistory;

public class GetPickupHistoryAsCollectorQueryHandler(IPickupServices pickupServices)
{
    public async Task<PagedResponse<PickupHistoryAsCollectorDto>> Handle(GetPickupHistoryAsCollectorQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var validator = new GetPickupHistoryAsCollectorQueryValidator();
        ValidationResult validationResult = await validator.ValidateAsync(query, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        int actualPageSize = query.PageSize > 10 ? 10 : query.PageSize;

        List<PickupHistoryAsCollectorDto> list =  await pickupServices.GetPickupHistoryAsCollector(
            query.Page,
            actualPageSize,
            string.IsNullOrWhiteSpace(query.Type) ? null : Enum.Parse<PickupType>(query.Type),
            string.IsNullOrWhiteSpace(query.Priority) ? null : Enum.Parse<Priority>(query.Priority),
            query.WasteContentName,
            string.IsNullOrWhiteSpace(query.FromDate) ? DateTime.MinValue : DateTime.Parse(query.FromDate, CultureInfo.InvariantCulture),
            string.IsNullOrWhiteSpace(query.ToDate) ? DateTime.MaxValue : DateTime.Parse(query.ToDate, CultureInfo.InvariantCulture),
            cancellationToken)
            .ConfigureAwait(false);

        return new PagedResponse<PickupHistoryAsCollectorDto>
        {
            Page = query.Page, PageSize = actualPageSize, Items = list
        };
    }
}
