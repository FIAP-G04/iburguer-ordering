using iBurguer.Orders.Infrastructure.IoC;
using iBurguer.Orders.Infrastructure.Logger;
using iBurguer.Orders.Infrastructure.PostgreSQL.Extensions;
using iBurguer.Orders.Infrastructure.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddWebApi()
    .AddPostgresSql()
    .AddRepositories()
    .AddUseCases()
    .AddSerilog();

app.Services.AddHealthCheck();

var app = builder.Build();

app.UseWebApi();
app.MapHealthChecks("/hc");
app.Run();