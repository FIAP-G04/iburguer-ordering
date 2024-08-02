using Amazon.SQS.Model;
using iBurguer.Ordering.Core.Abstractions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.EventHandlers;
using iBurguer.Ordering.Infrastructure.EventDispatcher;
using iBurguer.Ordering.Infrastructure.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace iBurguer.Ordering.Infrastructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class EventHandlerHostApplicationExtensions
    {
        public static IHostApplicationBuilder AddEventHandlers(this IHostApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddScoped<IEventDispatcher, EventDispatcher.EventDispatcher>();
            builder.Services.AddScoped<IEventHandler<OrderRegisteredDomainEvent>, OrderEventHandler>();
            builder.Services.AddScoped<ISQSService, SQSService>();
            builder.Services.Configure<SQSConfiguration>(configuration.GetRequiredSection("MassTransit"));

            return builder;
        }
    }
}
