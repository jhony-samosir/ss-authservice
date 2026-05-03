using SS.AuthService.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SS.AuthService.Application.Interfaces;

public interface IMfaRecoveryCodeRepository
{
    Task AddRangeAsync(IEnumerable<MfaRecoveryCode> codes, CancellationToken cancellationToken = default);
    Task<MfaRecoveryCode?> GetByHashAsync(string hash, CancellationToken cancellationToken = default);
    void Update(MfaRecoveryCode code);
}
