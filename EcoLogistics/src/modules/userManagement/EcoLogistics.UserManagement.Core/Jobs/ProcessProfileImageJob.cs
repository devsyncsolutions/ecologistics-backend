// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TickerQ.Utilities.Base;

namespace EcoLogistics.UserManagement.Core.Jobs;

public record ProcessProfileImageJobRequest(string UserId, IFormFile Image);

public class ProcessProfileImageJob(
    IProcessImagesService processImagesService,
    ILogger<ProcessProfileImageJob> logger,
    IUserManagementDbContext dbContext)
{
    [TickerFunction("ProcessProfileImageJob")]
    public async Task ProcessImageAsync(
        TickerFunctionContext<ProcessProfileImageJobRequest> context,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(processImagesService);

        string? imageUrl = await processImagesService
            .ProcessImageAsync(context.Request.Image, cancellationToken)
            .ConfigureAwait(false);

        if (imageUrl is null)
        {
            logger.LogErrorMessage(
                "Error processing profile image.",
                null,
                context.Request.UserId,
                null);

            return;
        }

        var user = await dbContext
            .ApplicationUsers
            .Where(u => u.UserName == context.Request.UserId)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            logger.LogWarningMessage(
                "Error updating profile image. User with ID {UserId} not found.",
                null,
                context.Request.UserId,
                null);

            return;
        }

        user.ProfileImageUrl = new Uri(imageUrl);
        dbContext.ApplicationUsers.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
