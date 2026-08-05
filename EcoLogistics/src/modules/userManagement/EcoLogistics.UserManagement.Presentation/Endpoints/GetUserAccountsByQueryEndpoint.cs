// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Presentation.Endpoints;

public sealed class GetUserAccountsByQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                pattern: "/user-management/users",
                handler: Handle)
            .WithName("GetUserAccountsByQuery")
            .WithDescription("Retrieves user account details.")
            .WithTags("User Management")
            .Produces<UserDetailsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromQuery] string? phoneNumber,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(phoneNumber))
            return Results.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = "Phone number is required."
            });

        var result = await bus.InvokeAsync<UserDetailsResponse>(new GetUserDetailsQuery{PhoneNumber = phoneNumber }, cancellationToken).ConfigureAwait(false);
        return Results.Ok(result);
    }
}

public record GetUserAccountsByQueryRequest(string? PhoneNumber);
