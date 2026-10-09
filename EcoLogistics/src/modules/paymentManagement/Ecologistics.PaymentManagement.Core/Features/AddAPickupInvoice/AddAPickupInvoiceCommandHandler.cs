// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.AddAPickupInvoice;

public class AddAPickupInvoiceCommandHandler(IPaymentManagementDbContext dbContext)
{
    public async Task Handle(AddAPickupInvoiceCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        string reference = await GenerateInvoiceReference(dbContext, cancellationToken).ConfigureAwait(false);

        var invoice = new PickupJobInvoice
        {
            Id = new PickupJobInvoiceId(Guid.CreateVersion7()),
            ReferenceNumber = reference,
            AmountPayable = command.Amount,
            InvoiceUrl = command.InvoiceUrl,
            ContentName = command.ContentName,
            AssociatedPickupId = command.PickupJobId
        };

        await dbContext.PickupJobInvoices.AddAsync(invoice, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task<string> GenerateInvoiceReference(IPaymentManagementDbContext paymentManagementDbContext, CancellationToken cancellationToken)
    {
        // Invoice reference format: INV-YYYY-NNNN

        string year = DateTime.UtcNow.Year.ToString(CultureInfo.InvariantCulture);

        string invoiceNumber = (
                await paymentManagementDbContext
                    .PickupJobInvoices
                    .CountAsync(cancellationToken)
                    .ConfigureAwait(false) + 1)
            .ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');

        return $"INV-{year}-{invoiceNumber}";
    }
}
