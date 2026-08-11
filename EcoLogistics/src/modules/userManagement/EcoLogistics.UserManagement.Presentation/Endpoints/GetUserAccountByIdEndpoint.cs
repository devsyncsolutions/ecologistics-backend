// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Contracts.Queries;

namespace EcoLogistics.UserManagement.Presentation.Endpoints;

public sealed class GetUserAccountByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                pattern: "/user-management/users/{userId}",
                handler: Handle)
            .WithName("GetUserAccountById")
            .WithDescription("Retrieves user account details by user ID.")
            .WithTags("User Management")
            .Produces<UserDetailsResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromRoute] string userId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        var result = await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery{UserId = userId }, cancellationToken).ConfigureAwait(false);
        return Results.Ok(result);
    }
}
