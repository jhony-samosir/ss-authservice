using Microsoft.AspNetCore.DataProtection;
using SS.AuthService.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace SS.AuthService.Infrastructure.Services;

public class DataEncryptionService : IDataEncryptionService
{
    private readonly IDataProtector _protector;

    public DataEncryptionService(IDataProtectionProvider provider)
    {
        // Purpose string should be constant for the application
        _protector = provider.CreateProtector("SS.AuthService.SecondarySensitiveData.v1");
    }

    public string Protect(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;
        return _protector.Protect(plainText);
    }

    public string Unprotect(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;
        
        try
        {
            return _protector.Unprotect(cipherText);
        }
        catch (CryptographicException)
        {
            // Log warning: Failed to decrypt. Data might be corrupted or key ring changed.
            return string.Empty;
        }
    }
}
