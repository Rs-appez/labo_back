using ParcBack.Domain.Entities;
using ParcBack.Application.EmployeeTasks;

namespace ParcBack.Application.Employees;

public static class Mappers
{
    public static EmployeeDto ToDto(this Employee employee) =>
        new(
            employee.Id,
            employee.Email,
            employee.IsActive,
            employee.Role,
            employee.CreatedAt,
            employee.LastLoginAt,
            [.. employee.Tasks.Select(t => t.ToDto())]
        );
}
