// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Presentation.Endpoints;

public sealed class UpdateUserProfileImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
            pattern: "/user-management/update-profile-image",
            handler: Handle)
            .WithName("UpdateUserProfileImage")
            .WithDescription("Updates logged in user's profile image.")
            .WithTags("User Management")
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> Handle(
        HttpContext httpContext,
        [FromServices] ITimeTickerManager<TimeTickerEntity> timeTicker,
        [FromServices] ILoggedInUserService loggedInUserService)
    {
        IFormFile? userProfileImage = httpContext.Request.Form.Files.FirstOrDefault(f => f.Name == "profilePicture");

        if (userProfileImage is null)
            return Results.BadRequest();

        TickerResult<TimeTickerEntity> result = await timeTicker.AddAsync(new TimeTickerEntity
        {
            Function = "ProcessImageAsync",
            ExecutionTime = DateTime.UtcNow.AddSeconds(5),
            Request = TickerHelper.CreateTickerRequest(new ProcessProfileImageJobRequest(loggedInUserService.GetUserId(), userProfileImage)),
            Retries = 3,
            RetryIntervals = [5, 10, 15]
        }, httpContext.RequestAborted).ConfigureAwait(false);

        return Results.Ok(result.Result.Id);
    }
}
