using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using ParcBack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository(AppDbContext db) => _db = db;

    public async Task<Role?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Roles.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<Role?> GetByNameAsync(string name, CancellationToken ct = default)
        => await _db.Roles.FirstOrDefaultAsync(t => t.Name == name, ct);

    public async Task<IReadOnlyList<Role>> ListAsync(CancellationToken ct = default)
        => await _db.Roles.AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(Role item, CancellationToken ct = default)
        => await _db.Roles.AddAsync(item, ct);

    public void Remove(Role item) => _db.Roles.Remove(item);

    public void Update(Role item) => _db.Roles.Update(item);

}
