using MediatR;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.Application.Auth.Commands;

public record EnableMfaCommand(int UserId, string Code) : IRequest<MfaEnableResult>;
