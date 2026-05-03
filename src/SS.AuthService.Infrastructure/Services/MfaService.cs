using OtpNet;
using QRCoder;
using SS.AuthService.Application.Interfaces;
using System;

namespace SS.AuthService.Infrastructure.Services;

public class MfaService : IMfaService
{
    private const string Issuer = "SamStore";

    public string GenerateSecret()
    {
        byte[] secretKey = KeyGeneration.GenerateRandomKey(20);
        return Base32Encoding.ToString(secretKey);
    }

    public string GenerateQrCodeUri(string email, string secret)
    {
        return $"otpauth://totp/{Issuer}:{email}?secret={secret}&issuer={Issuer}&digits=6";
    }

    public string GenerateQrCodeBase64(string uri)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeImage = qrCode.GetGraphic(20);
        return Convert.ToBase64String(qrCodeImage);
    }

    public bool VerifyCode(string secret, string code)
    {
        byte[] secretKey = Base32Encoding.ToBytes(secret);
        var totp = new Totp(secretKey);
        return totp.VerifyTotp(code, out long timeStepMatched, new VerificationWindow(1, 1));
    }
}
