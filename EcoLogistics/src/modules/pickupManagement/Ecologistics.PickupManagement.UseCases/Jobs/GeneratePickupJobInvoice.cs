// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.Jobs;

public record GeneratePickupJobInvoiceRequest(Guid PickupId);

public class GeneratePickupJobInvoice(
    IPickupManagementDbContext dbContext)
{
    public async Task GenerateInvoiceAndUpdateUrlAsync(
        TickerFunctionContext<GeneratePickupJobInvoiceRequest> context,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        Pickup pickupTruck = await dbContext
            .Pickups
            .AsTracking()
            .FirstAsync(p => p.Id == new PickupId(context.Request.PickupId), cancellationToken)
            .ConfigureAwait(false);

        string? invoiceUrl = string.Empty;

        _ = pickupTruck.UpdateInvoiceUrl(new Uri(invoiceUrl));
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
