using MediatR;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.Application.Auth.Commands;

public record RefreshTokenCommand(
    string RefreshToken,
    string IpAddress,
    string DeviceInfo) : IRequest<LoginResult>;
