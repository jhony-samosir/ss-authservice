using MediatR;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.Application.Auth.Commands;

public record SetupMfaCommand(int UserId) : IRequest<MfaSetupResult>;
