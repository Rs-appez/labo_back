using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using ParcBack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _db;

    public TaskRepository(AppDbContext db) => _db = db;

    public async Task<EmployeeTask?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Tasks.Include(t => t.Type)
                .Include(t => t.EmployeeAssigned)
                .FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<IReadOnlyList<EmployeeTask>> ListAsync(CancellationToken ct = default)
        => await _db.Tasks.Include(t => t.Type).AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(EmployeeTask item, CancellationToken ct = default)
        => await _db.Tasks.AddAsync(item, ct);

    public void Remove(EmployeeTask item) => _db.Tasks.Remove(item);

    public void Update(EmployeeTask item) => _db.Tasks.Update(item);

}
