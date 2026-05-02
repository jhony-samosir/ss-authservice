using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class mfa_recovery_code
{
    public long id { get; set; }

    public int user_id { get; set; }

    public string code_hash { get; set; } = null!;

    public bool is_used { get; set; }

    public DateTime? used_at { get; set; }
}
