using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.Employees.Login;

public class LoginHandler : IRequestHandler<LoginQuery, EmployeeDto?>
{
    private readonly IEmployeeRepository _repo;

    public LoginHandler(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<EmployeeDto?> Handle(LoginQuery command, CancellationToken ct)
    {
        Employee? employee = await _repo.GetByEmailAsync(command.Email, ct);
        if (employee == null || !BCrypt.Net.BCrypt.Verify(command.Password, employee.PasswordHash)) return null;

        employee.LastLoginAt = DateTime.UtcNow;
        _repo.Update(employee);
        

        return employee.ToDto();
    }

}
