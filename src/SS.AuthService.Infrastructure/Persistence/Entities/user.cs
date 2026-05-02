using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class user
{
    public int id { get; set; }

    public Guid public_id { get; set; }

    public int role_id { get; set; }

    public string email { get; set; } = null!;

    public string? password_hash { get; set; }

    public string full_name { get; set; } = null!;

    public bool is_active { get; set; }

    public bool mfa_enabled { get; set; }

    public string? mfa_secret { get; set; }

    public short failed_login_attempts { get; set; }

    public DateTime? locked_until { get; set; }

    public DateTime? email_verified_at { get; set; }

    public DateTime? tos_accepted_at { get; set; }

    public DateTime? privacy_policy_accepted_at { get; set; }

    public DateTime created_at { get; set; }

    public int? created_by { get; set; }

    public DateTime updated_at { get; set; }

    public int? updated_by { get; set; }

    public DateTime? deleted_at { get; set; }

    public int? deleted_by { get; set; }
}
