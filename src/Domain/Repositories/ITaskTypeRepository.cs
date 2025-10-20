using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;

public interface ITaskTypeRepository
{
    Task<TaskType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<TaskType?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<TaskType>> ListAsync(CancellationToken ct = default);
    Task AddAsync(TaskType zone, CancellationToken ct = default);
    void Update(TaskType zone);
    void Remove(TaskType zone);
}
