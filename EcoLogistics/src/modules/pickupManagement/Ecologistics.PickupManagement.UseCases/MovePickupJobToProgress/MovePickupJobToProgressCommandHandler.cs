// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using Ecologistics.PickupManagement.UseCases.Jobs;
using TickerQ.Utilities;

namespace Ecologistics.PickupManagement.UseCases.MovePickupJobToProgress;

public class MovePickupJobToProgressCommandHandler(
    IPickupManagementDbContext dbContext,
    IFusionCache cache,
    ITimeTickerManager<TimeTickerEntity> timeTicker)
{
    public async Task Handle(MovePickupJobToProgressCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cache.GetOrSetAsync(
            CacheKeys.MovePickupToProgress(command.PickupJobId),
            async _=> await RetrieveAndMovePickupToProgressAsync(dbContext, timeTicker, command, cancellationToken).ConfigureAwait(false),
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromHours(4),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(5)
            },
            cancellationToken).ConfigureAwait(false);
    }

    private static async Task<Guid> RetrieveAndMovePickupToProgressAsync(
        IPickupManagementDbContext dbContext,
        ITimeTickerManager<TimeTickerEntity> timeTicker,
        MovePickupJobToProgressCommand command,
        CancellationToken cancellationToken)
    {
        Pickup pickup = await dbContext.Pickups
            .AsTracking()
            .FirstAsync(p => p.Id == new PickupId(command.PickupJobId), cancellationToken)
            .ConfigureAwait(false);

        pickup.MoveToProgress();
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        await timeTicker.AddAsync(new TimeTickerEntity
        {
            Function = nameof(GeneratePickupJobInvoice.GenerateInvoiceAndUpdateUrlAsync),
            ExecutionTime = DateTime.UtcNow.AddSeconds(5),
            Request = TickerHelper.CreateTickerRequest(new GeneratePickupJobInvoiceRequest(pickup.Id.Value)),
            Retries = 3,
            RetryIntervals = [5, 10, 15]
        }, cancellationToken).ConfigureAwait(false);

        return pickup.Id.Value;
    }
}
