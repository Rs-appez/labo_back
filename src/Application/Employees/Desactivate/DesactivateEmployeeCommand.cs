using MediatR;

namespace ParcBack.Application.Employees.Desactivate;

public record DesactivateEmployeeCommand(
    Guid EmployeeId
) : IRequest<Unit>;
