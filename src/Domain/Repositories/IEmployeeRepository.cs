using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Employee?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<IReadOnlyList<Employee>> ListAsync(Guid? chefId = null, CancellationToken ct = default);
    Task Register(Employee employee, CancellationToken ct = default);
    void Update(Employee employee);
    void Remove(Employee employee);
}
