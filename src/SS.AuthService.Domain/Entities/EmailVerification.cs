using System;
using System.Collections.Generic;

namespace SS.AuthService.Domain.Entities;

public partial class EmailVerification
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public string VerificationTokenHash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
