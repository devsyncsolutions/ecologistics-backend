// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetPaymentDetails;

public class GetPaymentDetailsQueryHandler(
    IPaymentManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task<PaymentDetailsDto?> Handle(GetPaymentDetailsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var payment = await dbContext.Payments
            .Where(p =>
                p.Id == new PaymentId(request.PaymentId) &&
                (p.CollectorMemberId == loggedInUserService.GetUserId() || p.GeneratorId == loggedInUserService.GetUserId())
            )
            .Select(p => new{
                p.Id,
                p.AssociatedPickupId,
                p.Status,
                p.GeneratorId,
                p.CollectorMemberId,
                p.AmountPayedByGenerator,
                p.Commission,
                p.AmountPayedToCollectorTenant,
                p.PaymentMethodId,
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (payment is null)
            throw new NotFoundException("Payment not found");

        PaymentMethodType paymentMethodType = await dbContext.UserPaymentMethods
            .Where(pm => pm.Id == payment.PaymentMethodId)
            .Select(pm => pm.Method)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PaymentDetailsDto(
            payment.Id.ToString(),
            payment.AssociatedPickupId.ToString(),
            payment.Status.ToString(),
            payment.GeneratorId,
            payment.CollectorMemberId,
            paymentMethodType.ToString(),
            new MoneyDto(payment.AmountPayedByGenerator.Amount, payment.AmountPayedByGenerator.Currency.ToString()),
            new MoneyDto(payment.Commission.Amount, payment.Commission.Currency.ToString()),
            new MoneyDto(payment.AmountPayedToCollectorTenant.Amount, payment.AmountPayedToCollectorTenant.Currency.ToString())
        );
    }
}
