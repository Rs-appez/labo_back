using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;
public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Role?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<Role>> ListAsync(CancellationToken ct = default);
    Task AddAsync(Role zone, CancellationToken ct = default);
    void Update(Role zone);
    void Remove(Role zone);
}
