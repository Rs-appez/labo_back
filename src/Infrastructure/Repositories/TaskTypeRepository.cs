using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using ParcBack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Repositories;

public class TaskTypeRepository : ITaskTypeRepository
{
    private readonly AppDbContext _db;

    public TaskTypeRepository(AppDbContext db) => _db = db;

    public async Task<TaskType?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.TaskTypes.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<TaskType?> GetByNameAsync(string name, CancellationToken ct = default)
        => await _db.TaskTypes.FirstOrDefaultAsync(t => t.Name == name, ct);

    public async Task<IReadOnlyList<TaskType>> ListAsync(CancellationToken ct = default)
        => await _db.TaskTypes.AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(TaskType item, CancellationToken ct = default)
        => await _db.TaskTypes.AddAsync(item, ct);

    public void Remove(TaskType item) => _db.TaskTypes.Remove(item);

    public void Update(TaskType item) => _db.TaskTypes.Update(item);

}
