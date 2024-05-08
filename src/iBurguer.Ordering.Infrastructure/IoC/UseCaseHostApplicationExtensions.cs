using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using iBurguer.Ordering.Core.UseCases.CompleteOrder;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.Core.UseCases.DeliverOrder;
using iBurguer.Ordering.Core.UseCases.GetPagedOrders;
using iBurguer.Ordering.Core.UseCases.RegisterOrder;
using iBurguer.Ordering.Core.UseCases.StartOrder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Ordering.Infrastructure.IoC;

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
                        .AddScoped<IDeliverOrderUseCase, DeliverOrderUseCase>()
                        .AddScoped<IGetPagedOrdersUseCase, GetPagedOrdersUseCase>();

        return builder;
    }
}