using MediatR;
namespace ParcBack.Application.Employees.ListAllEmployees;

public record ListAllEmployeesQuery() : IRequest<IReadOnlyList<EmployeeDto>>;
