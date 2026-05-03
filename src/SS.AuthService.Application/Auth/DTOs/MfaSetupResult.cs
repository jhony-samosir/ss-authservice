namespace SS.AuthService.Application.Auth.DTOs;

public record MfaSetupResult(
    string Secret,
    string QrCodeUri,
    string QrCodeBase64);
