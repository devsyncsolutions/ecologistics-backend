// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CreateAPickupJob;

public record CreatPickupForQueryEntryCommand(Guid PickupId, Guid SagaId);
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
    public Guid? PickupId { get; set; }

    public static async Task<(PickupCreatedEventHandlerSaga, CreatPickupForQueryEntryCommand)> Start(
        PickupCreatedEvent @pickupCreatedEvent, IPickupManagementDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(@pickupCreatedEvent);
        ArgumentNullException.ThrowIfNull(dbContext);

        var saga = new PickupCreatedEventHandlerSaga
        {
            Id = Guid.NewGuid(),
            StartedAt = DateTimeOffset.UtcNow,
            PickupId = @pickupCreatedEvent.Id.Value
        };

        return (saga, new CreatPickupForQueryEntryCommand(@pickupCreatedEvent.Id.Value, saga.Id));
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
