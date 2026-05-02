using SS.AuthService.Application.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SS.AuthService.Infrastructure.Services;

public class BCryptPasswordHasher : IPasswordHasher
{
    // Work factor 12 adalah standar industri yang direkomendasikan OWASP (2024).
    // Nilai lebih tinggi = lebih aman tapi lebih lambat. Jangan turunkan di bawah 10.
    private const int WorkFactor = 12;

    public string HashPassword(string password)
        => BCryptNet.HashPassword(password, WorkFactor);

    public bool VerifyPassword(string password, string hash)
        => BCryptNet.Verify(password, hash);
}
