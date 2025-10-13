using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;
public interface IZoneRepository
{
    Task<Zone?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Zone?> GetByThemeAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<Zone>> ListAsync(CancellationToken ct = default);
    Task AddAsync(Zone zone, CancellationToken ct = default);
    void Update(Zone zone);
    void Remove(Zone zone);
}
