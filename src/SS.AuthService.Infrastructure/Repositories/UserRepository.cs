using Microsoft.EntityFrameworkCore;
using SS.AuthService.Application.Interfaces;
using SS.AuthService.Domain.Entities;
using SS.AuthService.Infrastructure.Persistence.Context;

namespace SS.AuthService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        // Menggunakan EF.Functions.ILike untuk case-insensitive comparison di PostgreSQL
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email && u.DeletedAt == null, cancellationToken);
    }

    public Task AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(entity);
        return Task.CompletedTask;
    }

    public async Task<int> GetDefaultCustomerRoleIdAsync(CancellationToken cancellationToken = default)
    {
        var role = await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == "Customer" && r.DeletedAt == null, cancellationToken);

        return role?.Id
            ?? throw new InvalidOperationException(
                "Default 'Customer' role not found. Ensure the database has been seeded properly.");
    }
}
