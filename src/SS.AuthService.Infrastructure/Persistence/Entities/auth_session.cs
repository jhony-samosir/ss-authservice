using System;
using System.Collections.Generic;
using System.Net;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class auth_session
{
    public long id { get; set; }

    public Guid public_id { get; set; }

    public int user_id { get; set; }

    public string refresh_token_hash { get; set; } = null!;

    public string? device_info { get; set; }

    public IPAddress? ip_address { get; set; }

    public DateTime expires_at { get; set; }

    public bool is_revoked { get; set; }

    public DateTime created_at { get; set; }

    public int? created_by { get; set; }

    public DateTime updated_at { get; set; }

    public int? updated_by { get; set; }
}
