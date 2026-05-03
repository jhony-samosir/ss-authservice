using MediatR;

namespace SS.AuthService.Application.Auth.Commands;

public record LogoutCommand(string RefreshToken) : IRequest<bool>;
