using System;
using System.Collections.Generic;

namespace SS.AuthService.Domain.Entities;

public partial class PasswordReset
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public string ResetTokenHash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UsedAt { get; set; }
}
