using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class HandleExceptionalMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HandleExceptionalMiddleware> _logger;

    public HandleExceptionalMiddleware(RequestDelegate next, ILogger<HandleExceptionalMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next middleware in the pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An unhandled exception has occurred.");

            // Handle the exception and return a consistent error response
            await HandleExceptionAsync(context, ex);
        }
    }

    //private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    //{
    //    // Set the response status code
    //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //    context.Response.ContentType = "application/json";

    //    // Create a consistent error response
    //    var response = new
    //    {
    //        StatusCode = context.Response.StatusCode,
    //        Message = "An unexpected error occurred. Please try again later.",
    //        Detailed = exception.Message // Include exception details (optional, for debugging)
    //    };

    //    // Serialize the response to JSON
    //    var jsonResponse = JsonSerializer.Serialize(response);

    //    // Write the JSON response to the HTTP response
    //    await context.Response.WriteAsync(jsonResponse);
    //}

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Handle specific exceptions
        switch (exception)
        {
            case ArgumentNullException _:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case UnauthorizedAccessException _:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception switch
            {
                ArgumentNullException _ => "Invalid input provided.",
                UnauthorizedAccessException _ => "Unauthorized access.",
                _ => "An unexpected error occurred. Please try again later."
            },
            Detailed = exception.Message // Include exception details (optional, for debugging)
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}