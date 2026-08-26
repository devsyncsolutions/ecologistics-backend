// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Services;

public class PickupServices(IPickupManagementDbContext dbContext) : IPickupServices
{
    public async Task<string> GenerateReference(string formattedAddress, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(formattedAddress);

        bool alreadyUsed = true;
        string reference = "";

        while (alreadyUsed)
        {
            string initial = formattedAddress.Length >= 2 ? formattedAddress[..2] : formattedAddress;
            reference = $"{initial.ToUpper()}-{CacheKeys.GenerateARegistrationNumber(4)}";
            alreadyUsed = await dbContext.Pickups
                .AnyAsync(p => p.Reference == reference, cancellationToken).ConfigureAwait(false);
        }

        return reference;
    }
}
