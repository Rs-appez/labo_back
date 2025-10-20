using MediatR;
namespace ParcBack.Application.TaskTypes.CreateTaskType;

public record CreateTaskTypeCommand(
    string Name
) : IRequest<int>;
