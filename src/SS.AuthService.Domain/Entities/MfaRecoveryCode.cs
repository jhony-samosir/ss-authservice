using System;
using System.Collections.Generic;

namespace SS.AuthService.Domain.Entities;

public partial class MfaRecoveryCode
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public string CodeHash { get; set; } = null!;

    public bool IsUsed { get; set; }

    public DateTime? UsedAt { get; set; }
}
