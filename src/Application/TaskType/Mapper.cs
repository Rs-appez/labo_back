using ParcBack.Domain.Entities;

namespace ParcBack.Application.TaskTypes;

public static class Mappers
{
    public static TaskTypeDto ToDto(this TaskType taskType) =>
        new TaskTypeDto(
            taskType.Id,
            taskType.Name
        );
}
