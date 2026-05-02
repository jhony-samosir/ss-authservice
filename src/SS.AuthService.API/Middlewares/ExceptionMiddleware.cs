using System.Net;
using System.Text.Json;
using FluentValidation;
using SS.AuthService.Application.Common.Exceptions;

namespace SS.AuthService.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            message = exception.Message,
            errors = new Dictionary<string, string[]>()
        };

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new
                {
                    message = "Validation Failed",
                    errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray())
                };
                break;
            case AppException appException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                _logger.LogError(exception, "An unhandled exception occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new
                {
                    message = "Internal Server Error. Please try again later.",
                    errors = new Dictionary<string, string[]>()
                };
                break;
        }

        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }
}
