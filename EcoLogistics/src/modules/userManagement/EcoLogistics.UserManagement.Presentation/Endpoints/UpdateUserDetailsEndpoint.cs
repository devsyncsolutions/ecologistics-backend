// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Presentation.Endpoints;

public sealed class UpdateUserDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
                pattern: "/user-management/update-details",
                handler: Handle)
            .WithName("UpdateUserDetails")
            .WithDescription("Updates logged in user's details.")
            .WithTags("User Management")
            .Accepts<UpdateUserDetailsCommand>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> Handle(
        [FromBody] UpdateUserDetailsCommand command,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        _= await bus.InvokeAsync<UpdateUserDetailsResponse>(command, cancellationToken).ConfigureAwait(false);
        return Results.NoContent();
    }
}
