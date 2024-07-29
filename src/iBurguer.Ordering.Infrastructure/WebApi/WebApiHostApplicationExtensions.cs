using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Infrastructure.SQS.Workers;
using iBurguer.Ordering.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Ordering.Infrastructure.WebApi;

[ExcludeFromCodeCoverage]
public static class WebApiHostApplicationExtensions
{
    public static IHostApplicationBuilder AddWebApi(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.AddSwagger();
        builder.Services.AddHealthChecks();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddHostedService<CartClosedWorker>();
        builder.Services.AddHostedService<PaymentConfirmedWorker>();
        builder.Services.AddHostedService<PaymentRefusedWorker>();

        return builder;
    }
    
    public static WebApplication UseWebApi(this WebApplication app)
    {
        app.ConfigureSwagger();
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.MapControllers();
        app.MapHealthChecks("/hc");

        return app;
    }
}

