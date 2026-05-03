using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SS.AuthService.API.Middlewares;

public class InternalHealthCheckMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public InternalHealthCheckMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/health/detail"))
        {
            // Security: Check for API Key or Localhost
            var apiKey = context.Request.Headers["X-Health-Key"].ToString();
            var expectedKey = _configuration["SecuritySettings:HealthCheckKey"] ?? "secret-health-key-123";

            var remoteIp = context.Connection.RemoteIpAddress;
            var isLocal = remoteIp != null && (remoteIp.Equals(context.Connection.LocalIpAddress) || System.Net.IPAddress.IsLoopback(remoteIp));

            if (apiKey != expectedKey && !isLocal)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Detailed health information is restricted.");
                return;
            }
        }

        await _next(context);
    }
}
