using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class email_verification
{
    public long id { get; set; }

    public int user_id { get; set; }

    public string verification_token_hash { get; set; } = null!;

    public DateTime expires_at { get; set; }

    public DateTime created_at { get; set; }
}
