var builder = WebApplication.CreateBuilder(args);

var app = builder
    .AddServices()
    .AddPipeline();

await app.RunAsync().ConfigureAwait(false);
