namespace SS.AuthService.API.DTOs;

public record MfaVerifyRequest(string MfaToken, string Code);
