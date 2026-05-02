using System;
using System.Collections.Generic;

namespace SS.AuthService.Domain.Entities;

public partial class PasswordHistory
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
