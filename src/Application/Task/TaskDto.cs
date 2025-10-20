using ParcBack.Application.TaskTypes;

namespace ParcBack.Application.EmployeeTasks;

public record EmployeeTaskDto(
    int Id,
    TaskTypeDto Type
);
