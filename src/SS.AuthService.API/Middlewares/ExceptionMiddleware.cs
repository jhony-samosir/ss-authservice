using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

        int statusCode = (int)HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred. Please try again later.";
        object? errors = null;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Validation Failed";
                errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                break;

            case AppException appException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = appException.Message;
                break;

            case DbUpdateException:
            case Npgsql.PostgresException:
                // Mask Database errors to prevent Information Exposure
                _logger.LogError(exception, "Database error occurred.");
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "A database error occurred. Detail masked for security.";
                break;

            default:
                var baseEx = exception.GetBaseException();
                if (baseEx is Npgsql.PostgresException)
                {
                    _logger.LogError(exception, "Database error detected via base exception.");
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "A database error occurred. Detail masked for security.";
                }
                else
                {
                    _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
                }
                break;
        }

        context.Response.StatusCode = statusCode;

        var response = new
        {
            success = false,
            message,
            errors = errors ?? new Dictionary<string, string[]>()
        };

        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }
}
