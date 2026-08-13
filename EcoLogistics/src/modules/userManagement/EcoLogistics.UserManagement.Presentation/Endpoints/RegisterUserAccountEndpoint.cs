// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Presentation.Endpoints;

public sealed class RegisterUserAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                pattern: "/user-management/register/as-generator",
                handler: Handle)
            .WithName("RegisterUserAccountAsGenerator")
            .WithDescription("Registers a new user account with the role of 'Generator'.")
            .WithTags("User Management")
            .Accepts<RegisterUserAccountCommand>("application/json")
            .Produces<RegisterUserAccountResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> Handle(
        [FromHeader(Name = "X-Request-ID")] string requestId,
        [FromBody] RegisterUserAccountRequest request,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(requestId))
        {
            return Results.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = "Missing or empty X-Request-ID header."
            });
        }

        var command = new RegisterUserAccountCommand(
            request.FirstName,
            request.LastName,
            request.OtherNames,
            request.Gender,
            request.PhoneNumber,
            request.DistrictId,
            AccountType.Generator);
        command.RequestId = requestId;

        var result = await bus.InvokeAsync<RegisterUserAccountResponse>(command, cancellationToken).ConfigureAwait(false);

        return Results.CreatedAtRoute(
            routeName: "GetUserAccountById",
            routeValues: new { userId = result.UserId },
            value: result);
    }
}
