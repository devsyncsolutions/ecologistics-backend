// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcoLogistics.UserManagement.Core.Features.StartLoginProcess;

namespace EcoLogistics.UserManagement.Presentation.Endpoints;

public sealed class StartLoginProcessEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                pattern: "/user-management/login/start",
                handler: Handle)
            .WithName("StartLoginProcess")
            .WithDescription("Starts the login process by generating an OTP code and sending it to the user's phone number.")
            .WithTags("User Management")
            .Accepts<LoginCommand>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> Handle(
        [FromHeader(Name = "X-Request-Id")] string requestId,
        [FromBody] LoginCommand command,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(requestId))
        {
            return Results.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = "Missing or empty X-Request-Id header."
            });
        }
        command.RequestId = requestId;

        _ = await bus.InvokeAsync<LoginResponse>(command, cancellationToken).ConfigureAwait(false);
        return Results.NoContent();
    }
}
