using ParcBack.Domain.Entities;
using ParcBack.Application.TaskTypes;

namespace ParcBack.Application.EmployeeTasks;

public static class Mappers
{
    public static EmployeeTaskDto ToDto(this EmployeeTask task)
        => new EmployeeTaskDto(
            task.Id,
            task.Type.ToDto()
        );
}
