using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Infrastructure.WebApi;

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
        OrderTrackingNotFoundException => StatusCodes.Status404NotFound,
        OrderNotFoundException => StatusCodes.Status404NotFound,
        CannotToStartOrderException => StatusCodes.Status422UnprocessableEntity,
        CannotToConfirmOrderException => StatusCodes.Status422UnprocessableEntity,
        CannotToCompleteOrderException => StatusCodes.Status422UnprocessableEntity,
        CannotToDeliverOrderException => StatusCodes.Status422UnprocessableEntity,
        CannotToCancelOrderException => StatusCodes.Status422UnprocessableEntity,
        ThePickupCodeCannotBeEmptyOrNullException => StatusCodes.Status422UnprocessableEntity,
        InvalidOrderNumberException => StatusCodes.Status422UnprocessableEntity,
        InvalidPriceException => StatusCodes.Status422UnprocessableEntity,
        InvalidQuantityException => StatusCodes.Status422UnprocessableEntity,
        ProductNameCannotBeNullOrEmptyException => StatusCodes.Status422UnprocessableEntity,
        LeastOneOrderItemException => StatusCodes.Status422UnprocessableEntity,

        _ => StatusCodes.Status500InternalServerError
    };
}