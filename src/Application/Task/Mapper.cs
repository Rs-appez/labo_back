using ParcBack.Domain.Entities;
using ParcBack.Application.TaskTypes;
using ParcBack.Application.Employees;

namespace ParcBack.Application.EmployeeTasks;

public static class Mappers
{
    public static EmployeeTaskDto ToDto(this EmployeeTask task)
        => new(
            task.Id,
            task.Type.ToDto(),
            task.EmployeeAssigned?.Id,
            task.IsCompleted,
            task.IsValidated,
            task.StartTime,
            task.EndTime
        );
}
