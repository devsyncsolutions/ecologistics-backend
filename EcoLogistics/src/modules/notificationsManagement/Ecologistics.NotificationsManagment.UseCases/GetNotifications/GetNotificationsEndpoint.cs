// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.NotificationsManagment.UseCases.GetNotifications;

public sealed class GetNotificationsEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/notifications", Handle)
            .WithName("GetNotifications")
            .WithDescription("Gets a list of notifications.")
            .WithTags("Notifications Management")
            .Produces<PagedResponse<NotificationDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromServices] IMessageBus bus,
        [FromQuery]int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool unseenOnly = true,
        CancellationToken cancellationToken = default)
    {
        var query = new GetNotificationsQuery(page, pageSize, unseenOnly);

        PagedResponse<NotificationDto> result = await bus.InvokeAsync<PagedResponse<NotificationDto>>(
            query,
            cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
