using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class menu
{
    public int id { get; set; }

    public Guid public_id { get; set; }

    public int? parent_id { get; set; }

    public string name { get; set; } = null!;

    public string path { get; set; } = null!;

    public string? icon { get; set; }

    public short sort_order { get; set; }

    public DateTime created_at { get; set; }

    public int? created_by { get; set; }

    public DateTime updated_at { get; set; }

    public int? updated_by { get; set; }

    public DateTime? deleted_at { get; set; }

    public int? deleted_by { get; set; }
}
