// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.UpdatePickup;

public sealed class UpdatePickupEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/pickup-management/pickup-jobs/{id:guid}", Handle)
            .WithName("UpdatePickup")
            .WithDescription("Updates a pickup job.")
            .WithTags("Pickup Management")
            .Accepts<UpdatePickupCommand>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(nameof(AccountType.Generator));
    }

    private static async Task<IResult> Handle(
        [FromBody] UpdatePickupCommand command,
        [FromRoute] Guid id,
        [FromServices] IMessageBus bus,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(bus);

        command.Id = id;
        await bus.InvokeAsync(command, cancellationToken).ConfigureAwait(false);
        return Results.NoContent();
    }
}
