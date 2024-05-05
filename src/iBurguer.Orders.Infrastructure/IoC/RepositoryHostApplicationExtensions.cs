using System.Diagnostics.CodeAnalysis;
using iBurguer.Orders.Core.Domain;
using iBurguer.Orders.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Orders.Infrastructure.IoC;

[ExcludeFromCodeCoverage]
public static class RepositoryHostApplicationExtensions
{
    public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        return builder;
    }
}