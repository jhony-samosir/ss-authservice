using System.Security.Cryptography;
using System.Text;
using SS.AuthService.Application.Interfaces;

namespace SS.AuthService.Infrastructure.Services;

/// <summary>
/// Menggunakan SHA-256 (deterministik) untuk token.
/// SHA-256 cocok untuk token yang perlu di-lookup dari DB karena menghasilkan hash yang sama
/// untuk input yang sama. BUKAN untuk password (gunakan Argon2id untuk password).
/// </summary>
public class TokenHasher : ITokenHasher
{
    public string Generate()
        => Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

    public string Hash(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
