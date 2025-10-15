using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;
public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Employee>> ListAsync(CancellationToken ct = default);
    Task Register(Employee zone, CancellationToken ct = default);
    void Update(Employee zone);
    void Remove(Employee zone);
}

