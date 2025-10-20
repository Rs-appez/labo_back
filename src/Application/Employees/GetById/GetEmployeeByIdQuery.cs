using MediatR;

namespace ParcBack.Application.Employees.GetEmployeeById;

public record GetEmployeeByIdQuery(
    Guid Id
) : IRequest<EmployeeDto?>;
