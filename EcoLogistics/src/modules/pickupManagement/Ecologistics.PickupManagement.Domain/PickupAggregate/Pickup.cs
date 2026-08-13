// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.PickupAggregate;

public class Pickup : BaseAggregate<PickupId>
{
    public PickupType Type { get; private set; } = PickupType.OneTime;
    public Point Coordinates { get; private set; } = default!;
    public string? FormattedAddress { get; private set; }
    public string? GhanaPostGpsAddress { get; private set; }
    public string? Landmark { get; private set; }
    public string? ContactName { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? ExtraNotes { get; private set; }
    public TimeStamp TimeStamp { get; private set; } = default!;
    public PickupFeeId PickupFeeId { get; private set; }
    public int NumberOfContainers { get; private set; } = 1;
    public PickupStatus PickupStatus { get; private set; }
    public string? CollectorMemberId { get; private set; }
    public Uri? DownloadableInvoiceUrl { get; private set; }
    public IsCancelled? IsCancelled { get; private set; }
    public string Reference { get; private set; } = string.Empty;
    public string WasteContentName { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.High;
    public RecurringProperties? RecurringProperties { get; private set; }

    public static Pickup Create(
        PickupFeeId pickupFeeId, PickupType type, double latitude, double longitude, string formattedAddress,
        string ghanaPostGpsAddress, string? landmark, string? contactName, string? contactPhone,
        string? extraNotes, DateTimeOffset scheduledForPickupAt, string wasteContentName, Priority? priority,
        string reference, int daysIntervalForPickupIfRecurring, int numberOfContainers)
    {
        var point = CreatePointCalculation.CreatePoint(longitude, latitude);
        var timeStamp = new TimeStamp(scheduledForPickupAt);

        var newPickup = new Pickup
        {
            Id = new PickupId(Guid.CreateVersion7()),
            Type = type,
            Coordinates = point,
            FormattedAddress = formattedAddress,
            GhanaPostGpsAddress = ghanaPostGpsAddress,
            Landmark = landmark,
            ContactName = contactName,
            ContactPhone = contactPhone,
            ExtraNotes = extraNotes,
            TimeStamp = timeStamp,
            PickupFeeId = pickupFeeId,
            PickupStatus = PickupStatus.Scheduled,
            Reference = reference,
            WasteContentName = wasteContentName,
            Priority = priority ?? Priority.High,
            NumberOfContainers = numberOfContainers
        };

        newPickup.RecurringProperties = type == PickupType.Recurring ?
            new RecurringProperties(daysIntervalForPickupIfRecurring, scheduledForPickupAt, newPickup.Id, true) :
            new RecurringProperties(1, scheduledForPickupAt, newPickup.Id, false);

        newPickup.AddDomainEvent(new PickupCreatedEvent(newPickup.Id));

        return newPickup;
    }

    public Pickup Start(string collectorMemberId)
    {
        if(string.IsNullOrWhiteSpace(collectorMemberId))
            throw new BadRequestException("Collector member id cannot be null or empty.");

        switch (PickupStatus)
        {
            case PickupStatus.Started:
                throw new ForbiddenRequestException("Pickup already started.");
            case PickupStatus.Completed:
                throw new ForbiddenRequestException("Pickup already completed.");
            case PickupStatus.Cancelled:
                throw new ForbiddenRequestException("Pickup already cancelled.");
        }

        PickupStatus = PickupStatus.Started;
        CollectorMemberId = collectorMemberId;

        var timeStamp = new TimeStamp(TimeStamp.ScheduledForPickupAt, DateTimeOffset.UtcNow);
        TimeStamp = timeStamp;

        AddDomainEvent(new PickupStartedEvent(Id));

        return this;
    }

    public void MoveToProgress()
    {
        PickupStatus = PickupStatus switch
        {
            PickupStatus.InProgress => throw new ForbiddenRequestException("Pickup already in progress."),
            PickupStatus.Scheduled => throw new ForbiddenRequestException("Pickup not started."),
            PickupStatus.Completed => throw new ForbiddenRequestException("Pickup already completed."),
            PickupStatus.Cancelled => throw new ForbiddenRequestException("Pickup already cancelled."),
            _ => PickupStatus.InProgress
        };
    }

    public void Complete()
    {
        switch (PickupStatus)
        {
            case PickupStatus.Completed:
                throw new ForbiddenRequestException("Pickup already completed.");
            case PickupStatus.Scheduled:
                throw new ForbiddenRequestException("Pickup not started.");
            case PickupStatus.Cancelled:
                throw new ForbiddenRequestException("Pickup already cancelled.");
        }

        PickupStatus = PickupStatus.Completed;

        var timeStamp = new TimeStamp(TimeStamp.ScheduledForPickupAt, TimeStamp.JobStartedAt, DateTimeOffset.UtcNow);
        TimeStamp = timeStamp;
    }

    public void Cancel(string reason, string cancelledBy)
    {
        switch (PickupStatus)
        {
            case PickupStatus.Cancelled:
                throw new ForbiddenRequestException("Pickup already cancelled.");
            case PickupStatus.Completed:
                throw new ForbiddenRequestException("Pickup already completed.");
            default:
                PickupStatus = PickupStatus.Cancelled;
                IsCancelled = new IsCancelled(DateTimeOffset.UtcNow, reason, cancelledBy);
                break;
        }
    }

    public Uri UpdateInvoiceUrl(Uri invoiceUrl)
    {
        DownloadableInvoiceUrl = invoiceUrl;
        return invoiceUrl;
    }

    public void UpdatePickup(string? contactName, string? contactPhone, string? extraNotes,
        DateTimeOffset? scheduledForPickupAt, Priority? priority, string wasteContentName)
    {
        if(scheduledForPickupAt is not null && PickupStatus != PickupStatus.Scheduled)
            throw new ForbiddenRequestException("You cannot update the scheduled pickup time after the pickup has started.");

        if(PickupStatus == PickupStatus.Completed)
            throw new ForbiddenRequestException("You cannot update the pickup details after the pickup has been completed.");

        ContactName = contactName;
        ContactPhone = contactPhone;
        ExtraNotes = extraNotes;
        Priority = priority ?? Priority;
        WasteContentName = wasteContentName;
        TimeStamp = scheduledForPickupAt is null ? TimeStamp : new TimeStamp(scheduledForPickupAt.Value);

        AddDomainEvent(new PickupUpdatedEvent(Id));
    }
}
