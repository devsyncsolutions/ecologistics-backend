// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.InitiateCharge;

public class InitiateChargeCommandHandler(
    IPaymentManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService,
    IFusionCache cache)
{
    public async Task<InitiateChargeResponse> Handle(InitiateChargeCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        InitiateChargeResponse response = await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<InitiateChargeCommand>(command.RequestId),
            async _ => await InitiateChargeAsync(dbContext, loggedInUserService, command, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromHours(1),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(1)
            },
            cancellationToken).ConfigureAwait(false);

        return response;
    }

    private static async Task<InitiateChargeResponse> InitiateChargeAsync(
        IPaymentManagementDbContext dbContext,
        ILoggedInUserService loggedInUserService,
        InitiateChargeCommand command,
        CancellationToken cancellationToken)
    {
        var existingPaymentByPickupId = await dbContext
            .Payments
            .Where(p => p.AssociatedPickupId == Guid.Parse(command.PickupId))
            .Select(p => new { Id = p.Id.Value.ToString(), Status = p.Status.ToString() })
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if(existingPaymentByPickupId is not null)
            return new InitiateChargeResponse(existingPaymentByPickupId.Id, existingPaymentByPickupId.Status);

        var validation = new InitiateChargeCommandValidator();
        ValidationResult validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        string generatorId = loggedInUserService.GetUserId();

        PickupJobInvoice? pickupInvoice = await dbContext
            .PickupJobInvoices
            .Where(pi =>
                pi.AssociatedPickupId == Guid.Parse(command.PickupId) &&
                pi.CreatedBy == generatorId)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickupInvoice is null)
            throw new NotFoundException("Invoice was not found");

        if (!string.IsNullOrWhiteSpace(command.PaymentMethodId))
        {
            bool methodExists = await dbContext
                .UserPaymentMethods
                .Where(pm =>
                    pm.Id == new UserPaymentMethodId(Guid.Parse(command.PaymentMethodId)) &&
                    pm.CreatedBy == generatorId)
                .AnyAsync(cancellationToken)
                .ConfigureAwait(false);

            if (!methodExists)
                throw new NotFoundException("Specified payment method was not found");
        }

        Payment newPayment = Payment.Create(
            pickupId: pickupInvoice.AssociatedPickupId,
            generator: generatorId,
            collector: pickupInvoice.CollectorMemberId,
            paymentMethodId: string.IsNullOrWhiteSpace(command.PaymentMethodId)
                ? null
                : new UserPaymentMethodId(Guid.Parse(command.PaymentMethodId)),
            amount: new Money(Currency.GHS, pickupInvoice.AmountPayable),
            commission: new Money(Currency.GHS, pickupInvoice.Commission));

        await dbContext.Payments.AddAsync(newPayment, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new InitiateChargeResponse(newPayment.Id.Value.ToString(), newPayment.Status.ToString());
    }
}
