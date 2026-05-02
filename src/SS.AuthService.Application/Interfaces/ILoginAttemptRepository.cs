using SS.AuthService.Domain.Entities;

namespace SS.AuthService.Application.Interfaces;

public interface ILoginAttemptRepository
{
    Task AddAsync(LoginAttempt attempt, CancellationToken cancellationToken = default);
}
