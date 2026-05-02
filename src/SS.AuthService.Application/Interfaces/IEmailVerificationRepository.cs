using SS.AuthService.Domain.Entities;

namespace SS.AuthService.Application.Interfaces;

public interface IEmailVerificationRepository
{
    /// <summary>Tambah token verifikasi email ke context (belum SaveChanges).</summary>
    Task AddAsync(EmailVerification entity, CancellationToken cancellationToken = default);

    /// <summary>Ambil record verifikasi berdasarkan hash token.</summary>
    Task<EmailVerification?> GetByTokenHashAsync(string hash, CancellationToken cancellationToken = default);

    /// <summary>Hapus record verifikasi.</summary>
    void Remove(EmailVerification entity);
}
