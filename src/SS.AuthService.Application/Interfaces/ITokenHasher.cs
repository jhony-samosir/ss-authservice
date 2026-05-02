namespace SS.AuthService.Application.Interfaces;

/// <summary>
/// Abstraksi untuk hashing token yang bersifat deterministik (SHA-256).
/// BERBEDA dari IPasswordHasher (BCrypt) yang menggunakan salt acak.
/// SHA-256 dibutuhkan agar token bisa di-lookup langsung dari database.
/// </summary>
public interface ITokenHasher
{
    /// <summary>Membuat token acak yang aman secara kriptografi.</summary>
    string Generate();

    /// <summary>Hash token menggunakan SHA-256 (deterministik, cocok untuk DB lookup).</summary>
    string Hash(string token);
}
