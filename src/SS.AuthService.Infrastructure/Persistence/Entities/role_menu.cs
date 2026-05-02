using System;
using System.Collections.Generic;

namespace SS.AuthService.Infrastructure.Persistence.Entities;

public partial class role_menu
{
    public int id { get; set; }

    public int role_id { get; set; }

    public int menu_id { get; set; }

    public bool can_create { get; set; }

    public bool can_read { get; set; }

    public bool can_update { get; set; }

    public bool can_delete { get; set; }

    public DateTime created_at { get; set; }

    public int? created_by { get; set; }

    public DateTime updated_at { get; set; }

    public int? updated_by { get; set; }

    public DateTime? deleted_at { get; set; }

    public int? deleted_by { get; set; }
}
