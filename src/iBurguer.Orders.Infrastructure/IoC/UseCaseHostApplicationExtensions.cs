using System.Diagnostics.CodeAnalysis;
using iBurguer.Orders.Core.UseCases.CancelOrder;
using iBurguer.Orders.Core.UseCases.CompleteOrder;
using iBurguer.Orders.Core.UseCases.ConfirmOrder;
using iBurguer.Orders.Core.UseCases.DeliverOrder;
using iBurguer.Orders.Core.UseCases.RegisterOrder;
using iBurguer.Orders.Core.UseCases.StartOrder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Orders.Infrastructure.IoC;

[ExcludeFromCodeCoverage]
public static class UseCaseHostApplicationExtensions
{
    public static IHostApplicationBuilder AddUseCases(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRegisterOrderUseCase, RegisterOrderUseCase>()
                        .AddScoped<IStartOrderUseCase, StartOrderUseCase>()
                        .AddScoped<ICancelOrderUseCase, CancelOrderUseCase>()
                        .AddScoped<IConfirmOrderUseCase, ConfirmOrderUseCase>()
                        .AddScoped<ICompleteOrderUseCase, CompleteOrderUseCase>()
                        .AddScoped<IDeliverOrderUseCase, DeliverOrderUseCase>();

        return builder;
    }
}