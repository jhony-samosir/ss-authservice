using System;
using System.Collections.Generic;
using System.Net;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class login_attempt
{
    public long id { get; set; }

    public int? user_id { get; set; }

    public string email_attempted { get; set; } = null!;

    public IPAddress ip_address { get; set; } = null!;

    public string? device_info { get; set; }

    public bool is_success { get; set; }

    public DateTime attempted_at { get; set; }
}
