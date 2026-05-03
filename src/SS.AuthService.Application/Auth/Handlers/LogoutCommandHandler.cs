using MediatR;
using SS.AuthService.Application.Auth.Commands;
using SS.AuthService.Application.Interfaces;

namespace SS.AuthService.Application.Auth.Handlers;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenHasher _tokenHasher;

    public LogoutCommandHandler(IUnitOfWork unitOfWork, ITokenHasher tokenHasher)
    {
        _unitOfWork = unitOfWork;
        _tokenHasher = tokenHasher;
    }

    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return true; // Already logged out or no session to revoke
        }

        var refreshTokenHash = _tokenHasher.Hash(request.RefreshToken);
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var session = await _unitOfWork.AuthSessions.GetByRefreshTokenHashAsync(refreshTokenHash, cancellationToken);

            if (session != null)
            {
                _unitOfWork.AuthSessions.Revoke(session);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
