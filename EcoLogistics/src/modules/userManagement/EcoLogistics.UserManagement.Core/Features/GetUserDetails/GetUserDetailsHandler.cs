// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;

namespace EcoLogistics.UserManagement.Core.Features.GetUserDetails;

public class GetUserDetailsHandler(
    IUserManagementDbContext dbContext,
    IFusionCache cache,
    ILoggedInUserService loggedInUserService)
{
    public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var requestResponse = await cache.GetOrSetAsync(
            CacheKeys.GetUserDetailsKey(query.UserId ?? query.PhoneNumber ?? loggedInUserService.GetUserId()),
            async _ => await GetUserDetailsAsync(dbContext, query.UserId, query.PhoneNumber, loggedInUserService.GetUserId(), cancellationToken).ConfigureAwait(false),
            TimeSpan.FromMinutes(120),
            cancellationToken).ConfigureAwait(false);

        return requestResponse;
    }

    private static async Task<UserDetailsResponse> GetUserDetailsAsync(
        IUserManagementDbContext dbContext,
        string? userId,
        string? phoneNumber,
        string loggedInUserId,
        CancellationToken cancellationToken)
    {
        var query = dbContext
            .ApplicationUsers
            .AsNoTracking();

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(u => u.UserName == userId);
        }
        else if (!string.IsNullOrEmpty(phoneNumber))
        {
            query = query.Where(u => u.PhoneNumber == phoneNumber);
        }
        else if (!string.IsNullOrWhiteSpace(loggedInUserId))
        {
            query = query.Where(u => u.UserName == loggedInUserId);
        }
        else
        {
            throw new InvalidOperationException("Either UserId or PhoneNumber must be provided.");
        }

        var user = await query
                .Select(u => new UserDetailsResponse(
                    u.FirstName,
                    u.LastName,
                    u.OtherNames ?? string.Empty,
                    u.PhoneNumber ?? string.Empty,
                    u.DateOfBirth != null ? u.DateOfBirth.Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) : string.Empty,
                    u.Gender.ToString(),
                    u.ProfileImageUrl != null ? u.ProfileImageUrl.ToString() : string.Empty,
                    u.Residence ?? string.Empty,
                    u.ResidenceGps ?? string.Empty
                ))
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        return user ?? throw new NotFoundException("User not found.");
    }
}
