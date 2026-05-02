namespace SS.AuthService.API.DTOs;

public record RegisterRequest(
    string Email,
    string Password,
    string FullName,
    bool AcceptTos,
    bool AcceptPrivacyPolicy);
