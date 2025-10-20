using MediatR;
namespace ParcBack.Application.TaskTypes.GetTaskTypeById;

public record GetTaskTypeByIdQuery(
    int Id
) : IRequest<TaskTypeDto?>;
