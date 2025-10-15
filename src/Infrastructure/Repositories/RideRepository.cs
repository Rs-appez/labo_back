using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using ParcBack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Repositories;

public class RideRepository : IRideRepository
{
    private readonly AppDbContext _db;

    public RideRepository(AppDbContext db) => _db = db;

    public async Task<Ride?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Rides.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<IReadOnlyList<Ride>> ListAsync(CancellationToken ct = default)
        => await _db.Rides.AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(Ride item, CancellationToken ct = default)
        => await _db.Rides.AddAsync(item, ct);

    public void Remove(Ride item) => _db.Rides.Remove(item);

    public void Update(Ride item) => _db.Rides.Update(item);

}

