using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Repositories;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Comment?>> GetByTaskIdAsync(int taskId, CancellationToken ct = default);
    Task<IReadOnlyList<Comment>> ListAsync(CancellationToken ct = default);
    Task AddAsync(Comment zone, CancellationToken ct = default);
    void Update(Comment zone);
    void Remove(Comment zone);
}
