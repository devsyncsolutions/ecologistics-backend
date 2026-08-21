// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace Ecologistics.PickupManagement.UseCases.UpdatePickup;

public class UpdatePickupCommandHandler(
    IPickupManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task Handle(UpdatePickupCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var validation = new UpdatePickupCommandValidator();
        ValidationResult validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Pickup? pickup = await dbContext.Pickups.Where(p => p.Id == new PickupId(command.Id) && p.CreatedBy == loggedInUserService.GetUserId())
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pickup is null)
            throw new NotFoundException($"Pickup with Id {command.Id} was not found");

        pickup.UpdatePickup(
            command.ContactName,
            command.ContactPhone,
            command.ExtraNotes,
            string.IsNullOrWhiteSpace(command.ScheduledForPickupAt)
                ? null
                : DateTimeOffset.Parse(command.ScheduledForPickupAt, CultureInfo.InvariantCulture),
            string.IsNullOrWhiteSpace(command.Priority) ? null : Enum.Parse<Priority>(command.Priority),
            command.WasteContentName);
    }
}
