using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;
using ParcBack.Domain.Abstractions;


namespace ParcBack.Application.Employees.Login;

public class LoginHandler : IRequestHandler<LoginQuery, EmployeeDto?>
{
    private readonly IEmployeeRepository _repo;
    private readonly IUnitOfWork _uow;

    public LoginHandler(IEmployeeRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<EmployeeDto?> Handle(LoginQuery command, CancellationToken ct)
    {
        Employee? employee = await _repo.GetByEmailAsync(command.Email, ct);
        if (employee == null || !BCrypt.Net.BCrypt.Verify(command.Password, employee.PasswordHash)) return null;

        employee.LastLoginAt = DateTime.UtcNow;
        _repo.Update(employee);
        await _uow.SaveChangesAsync(ct);



        return employee.ToDto();
    }

}
