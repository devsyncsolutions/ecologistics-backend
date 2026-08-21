// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetActivePickups;

public sealed class GetActivePickupsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-management/pickup-jobs/active-jobs/as-generator", Handle)
            .WithName("GetActivePickupJobs")
            .WithDescription("Gets active pickup jobs as a generator")
            .WithTags("Pickup Management")
            .Produces<IReadOnlyList<ActivePickupDto>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(nameof(AccountType.Generator));
    }

    private static async Task<IResult> Handle([FromServices] IMessageBus bus, CancellationToken cancellationToken)
    {
        var result = await bus
            .InvokeAsync<IReadOnlyList<ActivePickupDto>>(new GetActivePickupsQuery(), cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(result);
    }
}
