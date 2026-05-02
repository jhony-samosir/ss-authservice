using SS.AuthService.Application.Interfaces;
using SS.AuthService.Domain.Entities;
using SS.AuthService.Infrastructure.Persistence.Context;

namespace SS.AuthService.Infrastructure.Repositories;

public class LoginAttemptRepository : ILoginAttemptRepository
{
    private readonly AppDbContext _context;

    public LoginAttemptRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LoginAttempt attempt, CancellationToken cancellationToken = default)
    {
        await _context.LoginAttempts.AddAsync(attempt, cancellationToken);
    }
}
