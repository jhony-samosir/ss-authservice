using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class password_history
{
    public long id { get; set; }

    public int user_id { get; set; }

    public string password_hash { get; set; } = null!;

    public DateTime created_at { get; set; }
}
