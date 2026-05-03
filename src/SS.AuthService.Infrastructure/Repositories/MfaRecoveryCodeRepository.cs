using Microsoft.EntityFrameworkCore;
using SS.AuthService.Application.Interfaces;
using SS.AuthService.Domain.Entities;
using SS.AuthService.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SS.AuthService.Infrastructure.Repositories;

public class MfaRecoveryCodeRepository : IMfaRecoveryCodeRepository
{
    private readonly AppDbContext _context;

    public MfaRecoveryCodeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<MfaRecoveryCode> codes, CancellationToken cancellationToken = default)
    {
        await _context.MfaRecoveryCodes.AddRangeAsync(codes, cancellationToken);
    }

    public async Task<MfaRecoveryCode?> GetByHashAsync(string hash, CancellationToken cancellationToken = default)
    {
        return await _context.MfaRecoveryCodes
            .FirstOrDefaultAsync(x => x.CodeHash == hash && !x.IsUsed, cancellationToken);
    }

    public void Update(MfaRecoveryCode code)
    {
        _context.MfaRecoveryCodes.Update(code);
    }
}
