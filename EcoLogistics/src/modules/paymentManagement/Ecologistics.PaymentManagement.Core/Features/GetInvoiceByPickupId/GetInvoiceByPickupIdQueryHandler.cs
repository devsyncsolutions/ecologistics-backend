// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetInvoiceByPickupId;

public class GetInvoiceByPickupIdQueryHandler(
    IPaymentManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task<SingleInvoiceDto?> Handle(GetInvoiceByPickupIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        SingleInvoiceDto? pickupInvoice = await dbContext
            .PickupJobInvoices
            .Where(p => p.AssociatedPickupId == request.PickupId &&
                        p.CollectorMemberId == loggedInUserService.GetUserId())
            .Select(p => new SingleInvoiceDto(
                p.AmountPayable,
                p.Commission))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickupInvoice is null)
            throw new NotFoundException("Invoice was not found");

        return pickupInvoice;
    }
}
