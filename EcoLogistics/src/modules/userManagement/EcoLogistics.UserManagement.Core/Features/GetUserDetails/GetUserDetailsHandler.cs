// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Features.GetUserDetails;

public class GetUserDetailsHandler(
    IUserManagementDbContext dbContext,
    IFusionCache cache,
    ILoggedInUserService loggedInUserService,
    IMessageBus bus)
{
    public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var requestResponse = await cache.GetOrSetAsync(
            CacheKeys.GetUserDetailsKey(query.UserId ?? query.PhoneNumber ?? loggedInUserService.GetUserId()),
            async _ => await GetUserDetailsAsync(dbContext, bus, query.UserId, query.PhoneNumber, loggedInUserService.GetUserId(), loggedInUserService.GetDistrictId(), cancellationToken).ConfigureAwait(false),
            TimeSpan.FromMinutes(120),
            cancellationToken).ConfigureAwait(false);

        return requestResponse;
    }

    private static async Task<UserDetailsResponse> GetUserDetailsAsync(
        IUserManagementDbContext dbContext,
        IMessageBus bus,
        string? userId,
        string? phoneNumber,
        string loggedInUserId,
        string districtId,
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
        else if (!string.IsNullOrEmpty(loggedInUserId))
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
                    u.DateOfBirth != null ? u.DateOfBirth.Value.ToShortDateString() : string.Empty,
                    u.Gender.ToString(),
                    u.ProfileImageUrl != null ? u.ProfileImageUrl.ToString() : string.Empty,
                    u.Residence ?? string.Empty,
                    u.ResidenceGps ?? string.Empty
                )
                {
                    District = new DistrictDto(u.DistrictId.ToString(), string.Empty)
                })
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        if (user is null)
            throw new NotFoundException("User not found.");

        var district = await bus.InvokeAsync<GetDistrictResponse>(
            new GetDistrictQuery(string.IsNullOrWhiteSpace(districtId) ? user.District.Id : districtId),
            cancellationToken)
            .ConfigureAwait(false);

        if (district != null)
            user.District = new DistrictDto(district.DistrictId, district.DistrictName);
        else
            throw new NotFoundException("District not found.");


        return user;
    }
}
