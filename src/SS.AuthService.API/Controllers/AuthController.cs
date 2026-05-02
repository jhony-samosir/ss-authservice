using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SS.AuthService.API.DTOs;
using SS.AuthService.Application.Auth.Commands;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [EnableRateLimiting("StrictPolicy")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.Password,
            request.FullName,
            request.AcceptTos,
            request.AcceptPrivacyPolicy);

        var result = await _mediator.Send(command);
        
        if (result.Success)
        {
            return Ok(new { message = result.Message });
        }
        
        return BadRequest(new { message = result.Message });
    }

    [HttpPost("login")]
    [EnableRateLimiting("StrictPolicy")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginUserCommand(
            request.Email,
            request.Password,
            request.DeviceInfo,
            HttpContext.Connection.RemoteIpAddress?.ToString());

        var result = await _mediator.Send(command);

        if (!result.Success)
        {
            return result.StatusCode switch
            {
                429 => StatusCode(429, new { message = result.Message }),
                403 => StatusCode(403, new { message = result.Message }),
                _ => BadRequest(new { message = result.Message })
            };
        }

        // Set Refresh Token in HttpOnly Secure Cookie
        if (!string.IsNullOrEmpty(result.RefreshToken))
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Wajib HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);
        }

        return Ok(new
        {
            message = result.Message,
            accessToken = result.AccessToken
        });
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest(new { message = "Token is required." });
        }

        var command = new VerifyEmailCommand(token);
        var result = await _mediator.Send(command);

        return result switch
        {
            VerifyEmailResult.Success => Ok(new { message = "Email verified successfully. You can now login." }),
            VerifyEmailResult.TokenExpired => BadRequest(new { message = "Verification link has expired. Please request a new one." }),
            VerifyEmailResult.TokenNotFound => BadRequest(new { message = "Invalid verification link." }),
            _ => BadRequest(new { message = "An error occurred during verification." })
        };
    }
}
