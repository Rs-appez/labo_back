using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using ParcBack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _db;

    public EmployeeRepository(AppDbContext db) => _db = db;

    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Employees.Include(e => e.Role)
                              .Include(e => e.Tasks)
                                    .ThenInclude(t => t.Type)
                              .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<Employee?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await _db.Employees.Include(e => e.Role).Include(e => e.Tasks).FirstOrDefaultAsync(e => e.Email == email, ct);

    public async Task<IReadOnlyList<Employee>> ListAsync(Guid? chefId = null, CancellationToken ct = default)
        => await _db.Employees.Where(e => chefId == null || e.ChiefId == chefId)
                .Include(e => e.Role)
                .Include(e => e.Tasks)
                .AsNoTracking().ToListAsync(ct);

    public async Task Register(Employee item, CancellationToken ct = default)
        => await _db.Employees.AddAsync(item, ct);

    public void Remove(Employee item) => _db.Employees.Remove(item);

    public void Update(Employee item) => _db.Employees.Update(item);

}
