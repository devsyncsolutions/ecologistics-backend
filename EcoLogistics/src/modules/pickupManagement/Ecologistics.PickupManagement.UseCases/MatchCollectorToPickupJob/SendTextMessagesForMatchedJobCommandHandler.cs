namespace Ecologistics.PickupManagement.UseCases.MatchCollectorToPickupJob;

public class SendTextMessagesForMatchedJobCommandHandler(
    IMessageBus bus,
    ITextMessagingService textMessagingService
)
{
    public async Task<SendTextMessagesForMatchedJobCompletedCommand> Handle(
        SendTextMessagesForMatchedJobCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        UserDetailsResponse user = await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery { UserId = command.UserId }, cancellationToken).ConfigureAwait(false);
        UserDetailsResponse collector = await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery {UserId = command.TriggeredBy}, cancellationToken).ConfigureAwait(false);

        string message = $"""

                          Hi {user.FirstName},

                          Your pickup job with Reference {command.PickupReference} has been matched with:
                          Name: {collector.FirstName} {collector.LastName}
                          Contact Number: {collector.PhoneNumber}

                          Thank you for using EcoLogistics!
                          """;

        _= await textMessagingService.SendAsync(user.PhoneNumber, message, null, cancellationToken).ConfigureAwait(false);

        return new SendTextMessagesForMatchedJobCompletedCommand(command.SagaId, command.PickupId);
    }
}
