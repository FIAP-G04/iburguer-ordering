using iBurguer.Ordering.Infrastructure.IoC;
using iBurguer.Ordering.Infrastructure.Logger;
using iBurguer.Ordering.Infrastructure.PostgreSQL;
using iBurguer.Ordering.Infrastructure.PostgreSQL.Extensions;
using iBurguer.Ordering.Infrastructure.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddWebApi()
    .AddPostgresSql()
    .AddRepositories()
    .AddUseCases()
    .AddSerilog();

var app = builder.Build();

app.UseWebApi();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<Context>();
await context.Database.MigrateAsync();

app.Run();