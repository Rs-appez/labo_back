using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;
public interface IRideRepository
{
    Task<Ride?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Ride>> ListAsync(CancellationToken ct = default);
    Task AddAsync(Ride zone, CancellationToken ct = default);
    void Update(Ride zone);
    void Remove(Ride zone);
}

