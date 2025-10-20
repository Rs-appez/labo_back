using MediatR;

namespace ParcBack.Application.EmployeeTasks.CreateTask;

public record CreateTaskCommand(
    int TypeId,
    Guid EmployeeId
) : IRequest<int>;
