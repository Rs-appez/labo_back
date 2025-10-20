using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using ParcBack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _db;

    public CommentRepository(AppDbContext db) => _db = db;

    public async Task<Comment?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Comments.Include(c => c.Employee).Include(c => c.Task).FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<IReadOnlyList<Comment?>> GetByTaskIdAsync(int taskId, CancellationToken ct = default)
        => await _db.Comments.Include(c => c.Employee).Include(c => c.Task).Where(c => c.Task.Id == taskId).OrderByDescending(c => c.CreatedAt).ToListAsync(ct);

    public async Task<IReadOnlyList<Comment>> ListAsync(CancellationToken ct = default)
        => await _db.Comments.Include(c => c.Employee).Include(c => c.Task).AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(Comment item, CancellationToken ct = default)
        => await _db.Comments.AddAsync(item, ct);

    public void Remove(Comment item) => _db.Comments.Remove(item);

    public void Update(Comment item) => _db.Comments.Update(item);

}

