// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.CompleteAPickupJob;

public class CompletePickupJobCommandHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache,
    IMessageBus bus,
    ILoggedInUserService loggedInUserService)
{
    public async Task Handle(CompletePickupJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<CompletePickupJobCommand>(command.RequestId),
            async _ => await CompleteAsync(command, bus, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(30),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30)
            },
            cancellationToken).ConfigureAwait(false);
    }

    private async Task<int> CompleteAsync(CompletePickupJobCommand command, IMessageBus bus, CancellationToken cancellationToken)
    {
        Pickup? pickup = await dbContext.Pickups
            .AsTracking()
            .Where(p => p.Id == new PickupId(command.PickupId))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickup is null)
            throw new NotFoundException($"Pickup with Id {command.PickupId} was not found");

        PaymentStatus paymentStatus = await bus.InvokeAsync<PaymentStatus>(
            new GetPaymentStatusQuery(pickup.Id.Value), cancellationToken)
            .ConfigureAwait(false);

        if(paymentStatus is not (PaymentStatus.Success or PaymentStatus.CashConfirmed))
            throw new BadHttpRequestException("Cannot complete pickup job. Payment status is not success");

        pickup.Complete(loggedInUserService.GetUserId());
        return await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
