using System.Collections.Generic;

namespace SS.AuthService.Application.Auth.DTOs;

public record MfaEnableResult(
    bool Success,
    string Message,
    List<string>? RecoveryCodes = null);
