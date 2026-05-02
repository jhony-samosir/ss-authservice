using MediatR;
using SS.AuthService.Application.Auth.DTOs;

namespace SS.AuthService.Application.Auth.Commands;

public record VerifyEmailCommand(string Token) : IRequest<VerifyEmailResult>;
