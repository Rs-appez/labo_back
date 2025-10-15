using MediatR;
namespace ParcBack.Application.Employees.Login;

public record LoginQuery(string Email, string Password) : IRequest<EmployeeDto>;
