namespace SS.AuthService.API.DTOs;

public record LoginRequest(string Email, string Password, string? DeviceInfo = null);
