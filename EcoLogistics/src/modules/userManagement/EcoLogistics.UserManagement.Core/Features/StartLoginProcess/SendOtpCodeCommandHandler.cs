// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.StartLoginProcess;

public class SendOtpCodeCommandHandler(
    IUserManagementDbContext dbContext, ITextMessagingService textMessagingService)
{
    public async Task Handle(SendOtpCodeCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var user = await dbContext.ApplicationUsers
            .AsNoTracking()
            .Where(u => u.PhoneNumber == command.PhoneNumber)
            .Select(u => new { u.FirstName, u.LastName })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if(user is null)
            throw new ArgumentNullException($"User with phone number {command.PhoneNumber} not found.");

        string message = @$"
            Dear {user.FirstName} {user.LastName},
            Your OTP code for EcoLogistics login is: {command.OtpCode}.";

        await textMessagingService
            .SendAsync(command.PhoneNumber, message, command.RequestId, cancellationToken)
            .ConfigureAwait(false);
    }
}
