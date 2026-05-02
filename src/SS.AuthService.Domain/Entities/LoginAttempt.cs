using System;
using System.Collections.Generic;
using System.Net;

namespace SS.AuthService.Domain.Entities;

public partial class LoginAttempt
{
    public long Id { get; set; }

    public int? UserId { get; set; }

    public string EmailAttempted { get; set; } = null!;

    public IPAddress IpAddress { get; set; } = null!;

    public string? DeviceInfo { get; set; }

    public bool IsSuccess { get; set; }

    public DateTime AttemptedAt { get; set; }
}
