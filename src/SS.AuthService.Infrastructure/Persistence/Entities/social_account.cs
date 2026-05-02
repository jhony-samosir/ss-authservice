using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class social_account
{
    public long id { get; set; }

    public int user_id { get; set; }

    public string provider { get; set; } = null!;

    public string provider_account_id { get; set; } = null!;

    public DateTime created_at { get; set; }
}
