using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static iBurguer.Orders.Core.Exceptions;

namespace iBurguer.Orders.Infrastructure.WebApi;

[ExcludeFromCodeCoverage]
public sealed class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {exceptionMessage}, Occurred at: {time}",
            exception.Message, DateTime.UtcNow);

        int statusCode = GetStatusCodeFromException(exception);

        ProblemDetails problemDetails = new()
        {
            Type = "https://httpstatuses.com/" + statusCode,
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };
        
        problemDetails.Extensions.Add(new KeyValuePair<string, object?>("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier));
        
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private int GetStatusCodeFromException(Exception exception) => exception switch
    {
        OrderTrackingNotFound => StatusCodes.Status404NotFound,
        OrderNotFound => StatusCodes.Status404NotFound,
        CannotToStartOrder => StatusCodes.Status422UnprocessableEntity,
        CannotToConfirmOrder => StatusCodes.Status422UnprocessableEntity,
        CannotToCompleteOrder => StatusCodes.Status422UnprocessableEntity,
        CannotToDeliverOrder => StatusCodes.Status422UnprocessableEntity,
        CannotToCancelOrder => StatusCodes.Status422UnprocessableEntity,
        ThePickupCodeCannotBeEmptyOrNull => StatusCodes.Status422UnprocessableEntity,
        InvalidOrderNumber => StatusCodes.Status422UnprocessableEntity,
        InvalidPrice => StatusCodes.Status422UnprocessableEntity,
        InvalidQuantity => StatusCodes.Status422UnprocessableEntity,
        ProductNameCannotBeNullOrEmpty => StatusCodes.Status422UnprocessableEntity,
        LeastOneOrderItem => StatusCodes.Status422UnprocessableEntity,
        InvalidOrderType => StatusCodes.Status422UnprocessableEntity,

        _ => StatusCodes.Status500InternalServerError
    };
}