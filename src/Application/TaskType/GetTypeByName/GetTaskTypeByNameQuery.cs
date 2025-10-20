using MediatR;
namespace ParcBack.Application.TaskTypes.GetTaskTypeByName;

public record GetTaskTypeByNameQuery(
    string Name
) : IRequest<TaskTypeDto?>;
