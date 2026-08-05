// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EcoLogistics.UserManagement.Core.Features.EndLoginProcess;

namespace EcoLogistics.UserManagement.Presentation.Endpoints;

public sealed class EndLoginProcessEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                pattern: "/user-management/login/end",
                handler: Handle)
            .WithName("EndLoginProcess")
            .WithDescription("Ends the login process by validating the OTP code and generating an access token.")
            .WithTags("User Management")
            .Accepts<GetTokenCommand>("application/json")
            .Produces<GetTokenResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> Handle(
        [FromHeader(Name = "X-Request-ID")] string requestId,
        [FromBody] GetTokenCommand command,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(requestId))
        {
            return Results.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = "Missing or empty X-Request-ID header."
            });
        }

        command.RequestId = requestId;
        var result = await bus.InvokeAsync<GetTokenResponse>(command, cancellationToken).ConfigureAwait(false);

        return Results.Ok(result);
    }
}
