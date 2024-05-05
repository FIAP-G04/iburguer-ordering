using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Orders.Infrastructure.PostgreSQL.Extensions;

[ExcludeFromCodeCoverage]
public static class PostgreSqlDbHostApplicationExtensions
{
    public static IHostApplicationBuilder AddPostgresSql(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PostgreSql");

        builder.Services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(connectionString,
                assembly => assembly.MigrationsAssembly("iBurguer.Orders.Infrastructure"));
        });

        return builder;
    }
}