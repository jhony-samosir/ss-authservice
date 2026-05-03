using Microsoft.EntityFrameworkCore;
using SS.AuthService.Domain.Common;
using System.Linq.Expressions;

namespace SS.AuthService.Infrastructure.Persistence.Context;

public partial class AppDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        // Apply Global Query Filter for ISoftDelete entities
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(ISoftDelete.DeletedAt));
                var nullConstant = Expression.Constant(null, typeof(DateTime?));
                var comparison = Expression.Equal(property, nullConstant);
                var lambda = Expression.Lambda(comparison, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
