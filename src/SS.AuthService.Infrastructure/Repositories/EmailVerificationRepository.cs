using SS.AuthService.Application.Interfaces;
using SS.AuthService.Domain.Entities;
using SS.AuthService.Infrastructure.Persistence.Context;

namespace SS.AuthService.Infrastructure.Repositories;

public class EmailVerificationRepository : IEmailVerificationRepository
{
    private readonly AppDbContext _context;

    public EmailVerificationRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(EmailVerification entity, CancellationToken cancellationToken = default)
    {
        _context.EmailVerifications.Add(entity);
        return Task.CompletedTask;
    }
}
