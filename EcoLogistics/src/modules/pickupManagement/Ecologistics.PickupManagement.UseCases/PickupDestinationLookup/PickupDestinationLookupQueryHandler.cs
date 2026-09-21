// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.PickupDestinationLookup;

public class PickupDestinationLookupQueryHandler(IPickupManagementDbContext dbContext)
{
    public async Task<(double, double)> Handle(PickupDestinationLookupQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        Point? coordinates = await dbContext.Pickups
            .Where(p => p.Id == new PickupId(query.PickupId))
            .Select(p => p.Coordinates)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (coordinates is null)
            throw new NotFoundException("Pickup job was not found.");

        return (coordinates.Y, coordinates.X);
    }
}
