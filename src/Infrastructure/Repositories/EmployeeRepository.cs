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
        => await _db.Employees.FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<Employee?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await _db.Employees.FirstOrDefaultAsync(e => e.Email == email, ct);

    public async Task<IReadOnlyList<Employee>> ListAsync(CancellationToken ct = default)
        => await _db.Employees.AsNoTracking().ToListAsync(ct);

    public async Task Register(Employee item, CancellationToken ct = default)
        => await _db.Employees.AddAsync(item, ct);

    public void Remove(Employee item) => _db.Employees.Remove(item);

    public void Update(Employee item) => _db.Employees.Update(item);

}


