using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using ParcBack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Repositories;

public class ZoneRepository : IZoneRepository
{
    private readonly AppDbContext _db;

    public ZoneRepository(AppDbContext db) => _db = db;

    public async Task<Zone?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Zones.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<Zone?> GetByNameAsync(string name, CancellationToken ct = default)
        => await _db.Zones.FirstOrDefaultAsync(t => t.Name == name, ct);

    public async Task<IReadOnlyList<Zone>> ListAsync(CancellationToken ct = default)
        => await _db.Zones.AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(Zone item, CancellationToken ct = default)
        => await _db.Zones.AddAsync(item, ct);

    public void Remove(Zone item) => _db.Zones.Remove(item);

    public void Update(Zone item) => _db.Zones.Update(item);

}
