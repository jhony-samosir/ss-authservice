using System;
using System.Collections.Generic;
using SS.AuthService.Domain.Common;

namespace SS.AuthService.Domain.Entities;

public partial class Role : IAuditableEntity, ISoftDelete
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }
}
