using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class password_reset
{
    public long id { get; set; }

    public int user_id { get; set; }

    public string reset_token_hash { get; set; } = null!;

    public DateTime expires_at { get; set; }

    public bool is_used { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? used_at { get; set; }
}
