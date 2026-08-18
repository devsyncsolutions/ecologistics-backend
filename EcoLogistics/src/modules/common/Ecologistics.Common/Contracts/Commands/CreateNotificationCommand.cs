namespace Ecologistics.Common.Contracts.Commands;

public record CreateNotificationCommand(
    string TriggeredBy,
    string UserId, // owner of the notification
    string Title,
    string Body
);