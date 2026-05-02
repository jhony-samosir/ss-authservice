using System;
using System.Collections.Generic;
using System.Net;

namespace SS.AuthService.Domain.Entities;

public partial class AuthSession
{
    public long Id { get; set; }

    public Guid PublicId { get; set; }

    public int UserId { get; set; }

    public string RefreshTokenHash { get; set; } = null!;

    public string? DeviceInfo { get; set; }

    public IPAddress? IpAddress { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
