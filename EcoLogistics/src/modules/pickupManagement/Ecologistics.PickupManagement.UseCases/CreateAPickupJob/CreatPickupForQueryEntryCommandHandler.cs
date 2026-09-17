// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public class CreatPickupForQueryEntryCommandHandler(IPickupManagementDbContext dbContext)
{
    public async Task<PickupForQueryCreated> Handle(CreatPickupForQueryEntryCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(command);

        var fee = await dbContext
            .PickupFees
            .Where(pf => pf.Id == new PickupFeeId(command.PickupFeeId))
            .Select(pf => new
            {
                pf.Amount,
                pf.Commission,
                AvarageWeight = Math.Ceiling((pf.WasteMeasurement!.WeightRange.Max + pf.WasteMeasurement.WeightRange.Min) / 2),
                pf.WasteMeasurement.UnitOfMeasurement
            })
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);

        var pickupForQuery = new PickupForQuery
        {
            PickupId = command.PickupId,
            Reference = command.PickupReference,
            ContentName = command.ContactName ?? string.Empty,
            FormattedAddress = command.FormattedAddress ?? string.Empty,
            Type = command.Type,
            Priority = command.Priority,
            Amount = fee.Amount,
            Commission = fee.Commission,
            AverageWeight = $"{fee.AvarageWeight} {fee.UnitOfMeasurement}",
            Coordinates = command.Coordinates,
            NumberOfContainers = command.NumberOfContainers
        };

        await dbContext.PickupsForQuerying.AddAsync(pickupForQuery, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new PickupForQueryCreated(command.SagaId, pickupForQuery.Reference, command.CreatedBy);
    }
}
