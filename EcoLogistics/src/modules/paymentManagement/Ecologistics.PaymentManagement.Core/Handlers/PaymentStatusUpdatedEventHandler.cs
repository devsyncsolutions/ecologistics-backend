// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Handlers;

public class PaymentStatusUpdatedEventHandler(
    IMessageBus bus,
    IPaymentManagementDbContext dbContext)
{
    public async Task Handle(PaymentStatusUpdatedEvent @event, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(@event);

        // handling only cash confirmed payments for now
        if (@event.Status != PaymentStatus.CashConfirmed)
            return;

        var payment = await dbContext.Payments
            .Where(p => p.Id == new PaymentId(@event.PaymentId))
            .Select(p => new
            {
                p.CollectorMemberId,
                Commision = p.Commission.Amount
            })
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);

        await bus.InvokeAsync(new UpdateTenantBalanceCommand(payment.CollectorMemberId, payment.Commision), cancellationToken).ConfigureAwait(false);
    }
}
