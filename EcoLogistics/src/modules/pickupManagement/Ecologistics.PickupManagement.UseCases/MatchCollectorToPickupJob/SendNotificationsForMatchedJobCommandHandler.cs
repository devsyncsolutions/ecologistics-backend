using Ecologistics.Common.Contracts.Commands;

namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class SendNotificationsForMatchedJobCommandHandler(
    IPickupManagementDbContext dbContext,
    IMessageBus bus
)
{
    public async Task<SendNotificationsForMatchedJobCompletedCommand> Handle(SendNotificationsForMatchedJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var pickup = await dbContext
            .Pickups
            .Where(p => p.Id == command.Id)
            .Select(p => new
            {
                p.CollectorMemberId,
                p.CreatedBy,

            })
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);

        string title = "";
        string body = "";

        await bus.PublishAsync(new CreateNotificationCommand(
            TriggeredBy: pickup.CollectorMemberId!,
            UserId: pickup.CreatedBy!,
            Title: title,
            Body: body
        ))
        .ConfigureAwait(false);

        return new SendNotificationsForMatchedJobCompletedCommand(command.SagaId, command.Id);
    }
}