using Microsoft.EntityFrameworkCore;
using SS.AuthService.Application.Interfaces;
using SS.AuthService.Domain.Entities;
using SS.AuthService.Infrastructure.Persistence.Context;

namespace SS.AuthService.Infrastructure.Repositories;

public class AuthSessionRepository : IAuthSessionRepository
{
    private readonly AppDbContext _context;

    public AuthSessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AuthSession session, CancellationToken cancellationToken = default)
    {
        await _context.AuthSessions.AddAsync(session, cancellationToken);
    }

    public async Task<AuthSession?> GetByRefreshTokenHashAsync(string hash, CancellationToken cancellationToken = default)
    {
        return await _context.AuthSessions
            .FirstOrDefaultAsync(x => x.RefreshTokenHash == hash && !x.IsRevoked, cancellationToken);
    }

    public void Revoke(AuthSession session)
    {
        session.IsRevoked = true;
        _context.AuthSessions.Update(session);
    }
}
