namespace SS.AuthService.Application.Interfaces;

public interface IMfaService
{
    string GenerateSecret();
    string GenerateQrCodeUri(string email, string secret);
    string GenerateQrCodeBase64(string uri);
    bool VerifyCode(string secret, string code);
}
