using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Ordering.Infrastructure.IoC;

[ExcludeFromCodeCoverage]
public static class RepositoryHostApplicationExtensions
{
    public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        return builder;
    }
}