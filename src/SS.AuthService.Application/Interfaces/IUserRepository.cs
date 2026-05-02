using SS.AuthService.Domain.Entities;

namespace SS.AuthService.Application.Interfaces;

public interface IUserRepository
{
    /// <summary>Cek apakah email sudah terdaftar (case-insensitive). Aman dari Account Enumeration.</summary>
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>Tambah user baru ke context (belum SaveChanges).</summary>
    Task AddAsync(User entity, CancellationToken cancellationToken = default);

    /// <summary>Ambil ID role default "Customer" dari database. Mencegah magic number hardcoded.</summary>
    Task<int> GetDefaultCustomerRoleIdAsync(CancellationToken cancellationToken = default);
}
