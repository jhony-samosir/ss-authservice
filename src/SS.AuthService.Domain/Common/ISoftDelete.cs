using System;

namespace SS.AuthService.Domain.Common;

public interface ISoftDelete
{
    DateTime? DeletedAt { get; set; }
    int? DeletedBy { get; set; }
}
