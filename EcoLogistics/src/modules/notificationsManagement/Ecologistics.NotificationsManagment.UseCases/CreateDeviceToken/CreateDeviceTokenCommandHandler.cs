using Ecolgistics.NotificationsManagment.Infrastructure.Models;
using Ecolgistics.NotificationsManagment.Infrastructure.Services;
using Ecologistics.Common.Contracts.Commands;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecologistics.NotificationsManagment.UseCases;

public class CreateDeviceTokenCommandHandler(
    INotificationsManagementDbContext dbContext)
{
    public async Task Handle(CreateDeviceTokenCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.UserId))
            throw new ArgumentException("UserId is required.", nameof(command));

        if (string.IsNullOrWhiteSpace(command.Token))
            throw new ArgumentException("Token is required.", nameof(command));

        var normalizedToken = command.Token.Trim();

        var existingToken = await dbContext
            .DeviceTokens
            .FirstOrDefaultAsync(x =>
                x.UserId == command.UserId &&
                x.Token == normalizedToken,
                cancellationToken)
            .ConfigureAwait(false);

        if (existingToken is not null)
        {
            return;
        }

        var deviceToken = new DeviceToken
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Token = normalizedToken,
            Platform = command.DevicePlatform,
            RegisteredAt = DateTimeOffset.UtcNow
        };

        await dbContext.DeviceTokens
            .AddAsync(deviceToken, cancellationToken)
            .ConfigureAwait(false);

        await dbContext.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}