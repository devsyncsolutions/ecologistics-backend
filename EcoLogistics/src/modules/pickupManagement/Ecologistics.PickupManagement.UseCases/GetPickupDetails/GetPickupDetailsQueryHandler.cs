// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetPickupDetails;

public class GetPickupDetailsQueryHandler(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IMessageBus bus)
{
    public async Task<PickupDetailsResponse> Handle(GetPickupDetailsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await FetchDetailsAsync(dbContext, loggedInUserService, bus, query, cancellationToken)
            .ConfigureAwait(false);
    }

    private static async Task<PickupDetailsResponse> FetchDetailsAsync(IPickupManagementDbContext dbContext,
        ILoggedInUserService loggedInUserService, IMessageBus bus, GetPickupDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var pickup = await dbContext
            .Pickups
            .Where(p => p.Id == new PickupId(query.PickupId)
                        && (p.CreatedBy == loggedInUserService.GetUserId() ||
                            p.CollectorMemberId == loggedInUserService.GetUserId()))
            .Select(p => new
            {
                p.Type,
                p.Coordinates,
                p.FormattedAddress,
                p.GhanaPostGpsAddress,
                p.Landmark,
                p.ContactName,
                p.ContactPhone,
                p.ExtraNotes,
                TimeStamp = new
                {
                    p.TimeStamp.ScheduledForPickupAt,
                    p.TimeStamp.JobStartedAt,
                    p.TimeStamp.JobCompletedAt
                },
                p.PickupFeeId,
                p.NumberOfContainers,
                p.PickupStatus,
                p.CollectorMemberId,
                p.DownloadableInvoiceUrl,
                p.IsCancelled,
                p.Id,
                p.Reference,
                p.WasteContentName,
                p.Priority,
                NextScheduledDate = p.RecurringProperties == null ? DateTimeOffset.MinValue : p.RecurringProperties.NextScheduledDate,
                p.CreatedAt,
                p.CreatedBy
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickup is null)
            throw new NotFoundException("Pickup not found or you are not authorized to view this pickup");

        var pickupFee = await dbContext
            .PickupFees
            .Where(pf => pf.Id == pickup.PickupFeeId)
            .Select(pf => new
            {
                pf.Amount,
                pf.Commission,
                AverageWeight = Math.Ceiling((pf.WasteMeasurement!.WeightRange.Max + pf.WasteMeasurement.WeightRange.Min) / 2),
                pf.WasteMeasurement.UnitOfMeasurement,
                pf.WasteMeasurement.Name
            })
            .FirstAsync(cancellationToken).ConfigureAwait(false);

        UserDetailsResponse? collector = string.IsNullOrWhiteSpace(pickup.CollectorMemberId)
            ? null
            : await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = pickup.CollectorMemberId },
                cancellationToken)
                .ConfigureAwait(false);

        UserDetailsResponse generator = await bus
            .InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = pickup.CreatedBy },
                cancellationToken).ConfigureAwait(false);


        return new PickupDetailsResponse(
            pickup.Type.ToString(),
            pickup.FormattedAddress ?? string.Empty,
            pickup.GhanaPostGpsAddress ?? string.Empty,
            pickup.Landmark ?? string.Empty,
            pickup.ContactName ?? string.Empty,
            pickup.ContactPhone ?? string.Empty,
            pickup.ExtraNotes ?? string.Empty,
            new TimeStamp(
                pickup.TimeStamp.ScheduledForPickupAt.ToString(CultureInfo.InvariantCulture),
                pickup.TimeStamp.JobStartedAt?.ToString(CultureInfo.InvariantCulture),
                pickup.TimeStamp.JobCompletedAt?.ToString(CultureInfo.InvariantCulture)),
            new PickupFeeDto(
                pickupFee.Amount,
                pickupFee.Commission,
                pickup.NumberOfContainers,
                pickupFee.Name,
                $"{pickupFee.AverageWeight} {pickupFee.UnitOfMeasurement}"),
            pickup.PickupStatus.ToString(),
            pickup.CollectorMemberId ?? string.Empty,
            pickup.DownloadableInvoiceUrl == null ? string.Empty : pickup.DownloadableInvoiceUrl.ToString(),
            pickup.IsCancelled?.CancelledAt.ToString(CultureInfo.InvariantCulture),
            pickup.IsCancelled?.Reason,
            pickup.IsCancelled?.CancelledBy,
            pickup.Id.Value.ToString(),
            pickup.Reference,
            pickup.WasteContentName,
            pickup.Priority.ToString(),
            pickup.NextScheduledDate.ToString(CultureInfo.InvariantCulture),
            pickup.CreatedAt.ToString(CultureInfo.InvariantCulture),
            pickup.CreatedBy ?? string.Empty,
            pickup.Coordinates.X,
            pickup.Coordinates.Y,
            collector is null ? null : new UserDto(collector.PhoneNumber, $"{collector.FirstName} {collector.LastName}", collector.ProfileImage),
            new UserDto(generator.PhoneNumber, $"{generator.FirstName} {generator.LastName}", generator.ProfileImage)
        );
    }
}
