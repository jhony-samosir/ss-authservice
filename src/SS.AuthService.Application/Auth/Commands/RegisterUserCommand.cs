using MediatR;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.Application.Auth.Commands;

public record RegisterUserCommand(
    string Email,
    string Password,
    string FullName,
    bool AcceptTos,
    bool AcceptPrivacyPolicy) : IRequest<RegisterResult>;
