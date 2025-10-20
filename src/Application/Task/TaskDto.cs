using ParcBack.Application.TaskTypes;
using ParcBack.Application.Employees;

namespace ParcBack.Application.EmployeeTasks;

public record EmployeeTaskDto(
    int Id,
    TaskTypeDto Type,
    Guid EmployeeAssigned,
    bool IsCompleted,
    bool IsValidated
);
