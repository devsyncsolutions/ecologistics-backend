using EcoLogistics.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .AddServices()
    .AddPipeline();

await app.ApplyMigrationsAsync().ConfigureAwait(false);
await app.RunAsync().ConfigureAwait(false);
