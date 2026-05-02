namespace SS.AuthService.Application.Auth.DTOs;

public enum VerifyEmailResult
{
    Success,
    TokenNotFound,
    TokenExpired,
    UserNotFound
}
