using SS.AuthService.Domain.Entities;

namespace SS.AuthService.Application.Interfaces;

public interface IEmailVerificationRepository
{
    /// <summary>Tambah token verifikasi email ke context (belum SaveChanges).</summary>
    Task AddAsync(EmailVerification entity, CancellationToken cancellationToken = default);
}
