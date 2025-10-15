using ParcBack.Domain.Entities;

namespace ParcBack.Application.Employees;

public static class Mappers
{
    public static EmployeeDto ToDto(this Employee employee) =>
        new(
            employee.Id,
            employee.Email,
            employee.IsActive,
            employee.CreatedAt,
            employee.LastLoginAt
        );
}


