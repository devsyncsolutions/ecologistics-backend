// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Exceptions;
using Ecologistics.PickupManagement.Domain.PickupAggregate.Events;
using Microsoft.Extensions.Logging;
using Wolverine.Persistence.Sagas;

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public record CreatPickupForQueryEntryCommand(Pickup? Pickup, Guid SagaId);
public record PickupForQueryCreated([property: SagaIdentity] Guid SagaId, string PickupForQueryReference, string CreatedByUserId);
public record SendPickupCreatedNotification(string CreatedByUserId, string PickupReference, Guid SagaId);
public record PickupCreatedNotificationSent([property: SagaIdentity] Guid SagaId, string CreatedByUserId);

/// <summary>
/// Orchestrates the workflow for handling the PickupCreatedEvent.
///
/// Flow:
/// 1. Create a PickupForQuery entry in the database.
/// 2. Send a message notifying the user that the pickup has been created.
/// </summary>
public class PickupCreatedEventHandlerSaga : Saga
{
    public Guid Id { get; set; }
    public bool PickupForQueryCreated { get; set; }
    public bool PickupCreatedNotificationSent { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public Pickup? Pickup { get; set; }

    public static async Task<(PickupCreatedEventHandlerSaga, CreatPickupForQueryEntryCommand)> Start(
        PickupCreatedEvent @pickupCreatedEvent, IPickupManagementDbContext dbContext, ILogger<PickupCreatedEventHandlerSaga> logger)
    {
        ArgumentNullException.ThrowIfNull(@pickupCreatedEvent);
        ArgumentNullException.ThrowIfNull(dbContext);

        var pickup = await dbContext.Pickups
            .Where(p => p.Id == @pickupCreatedEvent.Id)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (pickup is null)
        {
            logger.LogErrorMessage(
                $"Pickup with Id {@pickupCreatedEvent.Id.Value} not found when starting PickupCreatedEvent saga.",
                null,
                null,
                null);

            throw new NotFoundException("Pickup not found.");
        }

        logger.LogInformationMessage(
            $"Starting saga for pickup created with Id {@pickupCreatedEvent.Id.Value}",
            null,
            pickup.CreatedBy,
            null);

        var saga = new PickupCreatedEventHandlerSaga
        {
            Id = Guid.NewGuid(),
            StartedAt = DateTimeOffset.UtcNow,
            Pickup = pickup
        };

        return (saga, new CreatPickupForQueryEntryCommand(pickup, saga.Id));
    }

    public SendPickupCreatedNotification Handle(PickupForQueryCreated @recordCreated)
    {
        ArgumentNullException.ThrowIfNull(@recordCreated);

        PickupForQueryCreated = true;
        return new SendPickupCreatedNotification(@recordCreated.CreatedByUserId, @recordCreated.PickupForQueryReference, Id);
    }

    public void Handle(PickupCreatedNotificationSent @recordCreated)
    {
        ArgumentNullException.ThrowIfNull(@recordCreated);

        PickupCreatedNotificationSent = true;
        MarkCompleted();
    }
}
