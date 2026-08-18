using Ecolgistics.NotificationsManagment.Infrastructure.Services;
using Ecologistics.Common.Contracts.Commands;
using Ecologistics.Common.Exceptions;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecologistics.NotificationsManagment.UseCases.CreateANotification;

public class CreateNotificationCommandHandler(
    INotificationsManagementDbContext dbContext, 
    FirebaseMessaging firebaseMessaging,
    ILogger<CreateNotificationCommandHandler> logger)
{
    public async Task Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var newNotification = new Ecolgistics.NotificationsManagment.Infrastructure.Models.Notification
        {
            Id = Guid.NewGuid(),
            TriggeredBy = command.TriggeredBy,
            UserId = command.UserId,
            Title = command.Title,
            Body = command.Body,
            CreatedAt = DateTimeOffset.UtcNow,
            IsSeen = false
        };

        await dbContext.Notifications.AddAsync(newNotification, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        var deviceTokens = await dbContext.DeviceTokens
            .Where(x => x.UserId == command.UserId)
            .Select(x => x.Token)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        if(deviceTokens.Count == 0) return;

#pragma warning disable CS0618 // Type or member is obsolete
        var message = new MulticastMessage
        {
            Tokens = deviceTokens,
            Notification = new Notification
            {
                Title = command.Title,
                Body = command.Body
            },
            Data = new Dictionary<string, string>
            {
                ["notificationId"] = newNotification.Id.ToString(),
                ["triggeredBy"] = command.TriggeredBy,
                ["userId"] = command.UserId
            }
        };
#pragma warning restore CS0618 // Type or member is obsolete

        try
        {
            BatchResponse response = await firebaseMessaging
                .SendEachForMulticastAsync(message, cancellationToken)
                .ConfigureAwait(false);

            var invalidTokens = response.Responses
                .Select((result, index) => new { result, index })
                .Where(x => !x.result.IsSuccess)
                .Select(x => deviceTokens[x.index])
                .ToList();

            if (invalidTokens.Count > 0)
            {
                var tokensToRemove = await dbContext.DeviceTokens
                    .Where(x => invalidTokens.Contains(x.Token))
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                dbContext.DeviceTokens.RemoveRange(tokensToRemove);
                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        } catch(Exception ex)
        {
            logger.LogErrorMessage(
                $"Error occured boadcasting notification with Firebase. Error: {ex.StackTrace!.ToString()}",
                null,
                command.UserId,
                null);
        }
    }
}