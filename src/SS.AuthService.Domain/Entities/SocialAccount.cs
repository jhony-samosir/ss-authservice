using System;
using System.Collections.Generic;

namespace SS.AuthService.Domain.Entities;

public partial class SocialAccount
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public string Provider { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
