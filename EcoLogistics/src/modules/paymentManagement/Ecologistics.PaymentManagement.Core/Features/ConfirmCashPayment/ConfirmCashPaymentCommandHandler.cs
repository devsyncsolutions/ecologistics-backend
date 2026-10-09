// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.ConfirmCashPayment;

public class ConfirmCashPaymentCommandHandler(
    IPaymentManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IFusionCache cache)
{
    public async Task<ConfirmCashPaymentResponse> Handle(ConfirmCashPaymentCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        ConfirmCashPaymentResponse response = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<ConfirmCashPaymentCommand>(command.RequestId),
            async _ => await ProcessConfirmCashPaymentAsync(dbContext, loggedInUserService, command, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromHours(1),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(1)
            },
        cancellationToken).ConfigureAwait(false);

        return response;
    }

    private static async ValueTask<ConfirmCashPaymentResponse> ProcessConfirmCashPaymentAsync(
        IPaymentManagementDbContext dbContext,
        ILoggedInUserService loggedInUserService,
        ConfirmCashPaymentCommand command,
        CancellationToken cancellationToken)
    {
        var validation = new ConfirmCashPaymentCommandValidator();
        ValidationResult validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingPaymentByPickupId = await dbContext
            .Payments
            .Where(p => p.AssociatedPickupId == Guid.Parse(command.PickupId))
            .Select(p => new { Id = p.Id.Value.ToString(), Status = p.Status.ToString() })
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if(existingPaymentByPickupId is not null)
            return new ConfirmCashPaymentResponse(existingPaymentByPickupId.Id, existingPaymentByPickupId.Status);

        string collectorMemberId = loggedInUserService.GetUserId();

        PickupJobInvoice? pickupInvoice = await dbContext
            .PickupJobInvoices
            .Where(pi =>
                pi.AssociatedPickupId == Guid.Parse(command.PickupId) &&
                pi.CollectorMemberId == collectorMemberId)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickupInvoice is null)
            throw new NotFoundException("Invoice was not found");

        UserPaymentMethodId? generatorCashPaymentMethodId = await dbContext
            .UserPaymentMethods
            .Where(upm =>
                upm.CreatedBy == pickupInvoice.CreatedBy &&
                upm.Method == PaymentMethodType.Cash)
            .Select(upm => upm.Id)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (generatorCashPaymentMethodId is null)
            throw new NotFoundException("Cash payment method was not found");

        Payment newPayment = Payment.Create(
            pickupInvoice.AssociatedPickupId,
            pickupInvoice.CreatedBy!,
            pickupInvoice.CollectorMemberId,
            generatorCashPaymentMethodId,
            new Money(Currency.GHS, pickupInvoice.AmountPayable),
            new Money(Currency.GHS, pickupInvoice.Commission),
            true);

        newPayment.UpdateStatus(PaymentStatus.CashConfirmed);

        await dbContext.Payments.AddAsync(newPayment, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new ConfirmCashPaymentResponse(newPayment.Id.Value.ToString(), newPayment.Status.ToString());
    }
}
