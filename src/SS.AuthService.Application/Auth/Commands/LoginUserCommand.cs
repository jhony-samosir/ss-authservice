using MediatR;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.Application.Auth.Commands;

public record LoginUserCommand(
    string Email, 
    string Password, 
    string? DeviceInfo = null, 
    string? IpAddress = null) : IRequest<LoginResult>;
