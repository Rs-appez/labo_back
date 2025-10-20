using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;

public interface ITaskRepository
{
    Task<EmployeeTask?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<EmployeeTask>> ListAsync(CancellationToken ct = default);
    Task AddAsync(EmployeeTask zone, CancellationToken ct = default);
    void Update(EmployeeTask zone);
    void Remove(EmployeeTask zone);
}
