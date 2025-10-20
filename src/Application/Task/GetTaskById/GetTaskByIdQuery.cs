using MediatR;

namespace ParcBack.Application.EmployeeTasks.GetTaskById;

public record GetTaskByIdQuery(
    int Id
) : IRequest<EmployeeTaskDto?>;
