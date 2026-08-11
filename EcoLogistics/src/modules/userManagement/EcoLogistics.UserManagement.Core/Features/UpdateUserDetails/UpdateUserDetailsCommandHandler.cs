// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;

namespace EcoLogistics.UserManagement.Core.Features.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(
    IUserManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task<UpdateUserDetailsResponse> Handle(UpdateUserDetailsCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var validation = new UpdateUserDetailsCommandValidator();
        var validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var existingUser = await dbContext.ApplicationUsers
            .FirstOrDefaultAsync(u => u.UserName == loggedInUserService.GetUserId(), cancellationToken)
            .ConfigureAwait(false);

        if (existingUser is null)
            throw new NotFoundException($"User with ID {loggedInUserService.GetUserId()} not found.");

        existingUser.FirstName = command.FirstName;
        existingUser.LastName = command.LastName;
        existingUser.OtherNames = command.OtherNames;
        existingUser.Gender = Enum.Parse<Gender>(command.Gender);
        existingUser.DateOfBirth =
            command.DateOfBirth is not null
                ? DateOnly.Parse(command.DateOfBirth, CultureInfo.CurrentCulture)
                : null;
        existingUser.Residence = command.Residence;
        existingUser.ResidenceGps = command.ResidenceGps;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new UpdateUserDetailsResponse(existingUser.Id);
    }
}
