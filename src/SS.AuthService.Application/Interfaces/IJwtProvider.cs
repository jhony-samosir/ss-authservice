using SS.AuthService.Domain.Entities;

namespace SS.AuthService.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    string GenerateMfaChallengeToken(User user);
    int? ValidateMfaChallengeToken(string token);
}
