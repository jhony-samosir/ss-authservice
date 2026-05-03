using MediatR;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.Application.Auth.Commands;

public record VerifyMfaCommand(
    string MfaToken, 
    string Code,
    string IpAddress,
    string DeviceInfo) : IRequest<LoginResult>;
