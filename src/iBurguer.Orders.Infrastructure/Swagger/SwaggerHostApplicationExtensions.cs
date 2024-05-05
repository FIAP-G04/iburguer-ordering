using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace iBurguer.Orders.Infrastructure.Swagger;

[ExcludeFromCodeCoverage]
public static class SwaggerHostApplicationExtensions
{
    private const string Title = "iBurguer Orders API";
    private const string Description = "The Order Lifecycle Management API is a vital component of the iBurger platform, exclusively developed for Byte Burguer. This RESTful API provides a comprehensive solution for managing the lifecycle of orders, from creation to delivery to the customer. ";
    private const string Version = "v1";
    
    public static IHostApplicationBuilder AddSwagger(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(Version, new OpenApiInfo
            {
                Title = Title, 
                Description = Description, 
                Version = Version
            });
            
            //options.ExampleFilters();
            options.EnableAnnotations();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "iBurguer.Orders.API.xml"));
            options.DescribeAllParametersInCamelCase();
        });

        return builder;
    }
    
    public static void ConfigureSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Title} {Version}");
            c.DisplayRequestDuration();
        });
    }
}