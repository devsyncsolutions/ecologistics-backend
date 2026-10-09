// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.Jobs;

public record GeneratePickupJobInvoiceRequest(Guid PickupId);

public class GeneratePickupJobInvoice(
    IPickupManagementDbContext dbContext,
    IProcessFilesService processFilesService,
    IMessageBus bus)
{
    public async Task GenerateInvoiceAndUpdateUrlAsync(
        TickerFunctionContext<GeneratePickupJobInvoiceRequest> context,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        Pickup pickup = await dbContext
            .Pickups
            .AsTracking()
            .FirstAsync(p => p.Id == new PickupId(context.Request.PickupId), cancellationToken)
            .ConfigureAwait(false);

        var averageWeight = await dbContext.PickupFees
            .Where(pf => pf.Id == pickup.PickupFeeId)
            .Select(pf => new
            {
                AverageWeight = (pf.WasteMeasurement!.WeightRange.Max + pf.WasteMeasurement.WeightRange.Min) / 2,
                pf.WasteMeasurement.UnitOfMeasurement,
                pf.Amount,
                pf.WasteMeasurement.Name
            })
            .FirstAsync(cancellationToken).ConfigureAwait(false);

        UserDetailsResponse collector = await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = pickup.CollectorMemberId }, cancellationToken)
            .ConfigureAwait(false);
        UserDetailsResponse generator = await bus
            .InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = pickup.CreatedBy }, cancellationToken)
            .ConfigureAwait(false);

        decimal totalPrice = averageWeight.Amount * pickup.NumberOfContainers;

        byte[] invoice = GenerateInvoiceForPickupService.Generate(
            new GenerateInvoiceForPickupRequest(
                PickupReference: pickup.Reference,
                WasteContent: pickup.WasteContentName,
                NumberOfPackages: pickup.NumberOfContainers,
                WeightForEachPackage: $"{averageWeight.AverageWeight} {averageWeight.UnitOfMeasurement}",
                TotalWeight: $"{averageWeight.AverageWeight *  pickup.NumberOfContainers} {averageWeight.UnitOfMeasurement}",
                PricePerPackage: averageWeight.Amount,
                PackageSize: averageWeight.Name,
                TotalPrice:  totalPrice,
                PickupDate: pickup.TimeStamp.ScheduledForPickupAt,
                PaymentMethod: string.Empty,
                CollectorFullName: $"{collector.FirstName} {collector.LastName}",
                GeneratorFullName: $"{generator.FirstName} {generator.LastName}"));

        string invoiceName = $"{pickup.Reference}_invoice_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        string? invoiceUrl = await processFilesService
            .ProcessPdfAsync(invoiceName, invoice, cancellationToken)
            .ConfigureAwait(false);

        await bus
            .InvokeAsync(
                new AddAPickupInvoiceCommand(
                    pickup.Id.Value, totalPrice,
                    string.IsNullOrEmpty(invoiceUrl) ? null : new Uri(invoiceUrl),
                    pickup.WasteContentName),
                cancellationToken).ConfigureAwait(false);
    }
}
