using iBurguer.Ordering.Infrastructure.IoC;
using iBurguer.Ordering.Infrastructure.Logger;
using iBurguer.Ordering.Infrastructure.PostgreSQL.Extensions;
using iBurguer.Ordering.Infrastructure.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddWebApi()
    .AddPostgresSql()
    .AddRepositories()
    .AddUseCases()
    .AddSerilog();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseWebApi();
app.MapHealthChecks("/hc");
app.Run();