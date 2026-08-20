// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.NotificationsManagment.UseCases.DeleteNotificationCompletely;

public sealed class DeleteNotificationCompletelyCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/notifications/{notificationId:guid}", Handle)
            .WithName("DeleteNotificationCompletely")
            .WithDescription("Deletes a notification completely.")
            .WithTags("Notifications Management")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid notificationId,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        var command = new DeleteNotificationCompletelyCommand(notificationId);
        await bus.InvokeAsync(command, cancellationToken).ConfigureAwait(false);
        return Results.Ok();
    }
}
