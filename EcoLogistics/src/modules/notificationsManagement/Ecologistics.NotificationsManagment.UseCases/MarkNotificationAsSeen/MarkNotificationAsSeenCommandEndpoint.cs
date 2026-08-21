// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.NotificationsManagment.UseCases.MarkNotificationAsSeen;

public sealed class MarkNotificationAsSeenCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/notifications/{notificationId:guid}/seen", Handle)
            .WithName("MarkNotificationAsSeen")
            .WithDescription("Marks a notification as seen.")
            .WithTags("Notifications Management")
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid notificationId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        var command = new MarkNotificationAsSeenCommand(notificationId);
        await bus.InvokeAsync(command, cancellationToken).ConfigureAwait(false);
        return Results.Ok();
    }
}
