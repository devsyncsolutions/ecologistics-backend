// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public class CreatPickupForQueryEntryCommandHandler(
    IPickupManagementDbContext dbContext)
{
    public async Task<PickupForQueryCreated> Handle(CreatPickupForQueryEntryCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(command.Pickup);

        var pickup = command.Pickup;

        var fee = await dbContext
            .PickupFees
            .Where(pf => pf.Id == pickup.PickupFeeId)
            .Select(pf => new
            {
                pf.Amount,
                pf.Commission,
                AvarageWeight = Math.Ceiling((pf.WasteMeasurement!.WeightRange.Max + pf.WasteMeasurement.WeightRange.Min) / 2),
                pf.WasteMeasurement.UnitOfMeasurement
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        // send a compensation event for when fee is null

        var pickupForQuery = new PickupForQuery
        {
            PickupId = pickup.Id.Value,
            Reference = pickup.Reference,
            ContentName = pickup.ContactName ?? string.Empty,
            FormattedAddress = pickup.FormattedAddress ?? string.Empty,
            Type = pickup.Type.ToString(),
            Priority = pickup.Priority.ToString(),
            Amount = fee!.Amount,
            Commission = fee.Commission,
            AverageWeight = $"{fee.AvarageWeight} {fee.UnitOfMeasurement}",
            Coordinates = pickup.Coordinates
        };

        await dbContext.PickupsForQuerying.AddAsync(pickupForQuery, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new PickupForQueryCreated(command.SagaId, pickupForQuery.Reference, pickup.CreatedBy!);
    }
}
