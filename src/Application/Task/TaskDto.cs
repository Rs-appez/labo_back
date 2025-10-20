using ParcBack.Application.TaskTypes;

namespace ParcBack.Application.EmployeeTasks;

public record EmployeeTaskDto(
    int Id,
    TaskTypeDto Type,
    Guid EmployeeAssigned,
    bool IsCompleted,
    bool IsValidated,
    DateTime StartTime,
    DateTime EndTime
);
