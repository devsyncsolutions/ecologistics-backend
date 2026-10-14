// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.StartAScheduledJob;

public class CreateAnotherPickupJobIfJobIsRecurringCommandHandler(
    IPickupManagementDbContext dbContext,
    IPickupServices pickupServices)
{
    public async Task Handle(CreateAnotherPickupJobIfJobIsRecurringCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if(command.Type != PickupType.Recurring)
            return;

        Pickup pickup = await dbContext
            .Pickups
            .Where(p => p.Id == command.PickupId)
            .FirstAsync(cancellationToken).ConfigureAwait(false);

        List<PickupStatus> statuses = [PickupStatus.Scheduled, PickupStatus.Matched];
        if (statuses.Contains(pickup.PickupStatus))
            return;

        string reference = await pickupServices.GenerateReference(pickup.FormattedAddress!, cancellationToken)
            .ConfigureAwait(false);

        var newRecurringPickup = Pickup.Create(
            pickup.PickupFeeId,
            pickup.Type,
            0,
            0,
            pickup.FormattedAddress ?? string.Empty,
            pickup.GhanaPostGpsAddress ?? string.Empty,
            pickup.Landmark,
            pickup.ContactName,
            pickup.ContactPhone,
            pickup.ExtraNotes,
            pickup.RecurringProperties!.NextScheduledDate,
            pickup.WasteContentName,
            pickup.Priority,
            reference,
            pickup.RecurringProperties.DaysIntervalForPickupIfRecurring,
            pickup.NumberOfContainers,
            pickup.CreatedBy!);

        newRecurringPickup.UpdateCollectorMemberId(pickup.CollectorMemberId!);
        newRecurringPickup.UpdateParentPickupId(pickup.Id);

        await dbContext.Pickups.AddAsync(newRecurringPickup, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
