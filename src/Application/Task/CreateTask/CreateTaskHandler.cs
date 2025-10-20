using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.EmployeeTasks.CreateTask;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, int>
{
    private readonly ITaskRepository _repo;
    private readonly ITaskTypeRepository _taskTypeRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IUnitOfWork _uow;

    public CreateTaskHandler(ITaskRepository repo, IUnitOfWork uow,
            ITaskTypeRepository taskTypeRepo, IEmployeeRepository employeeRepo)
    {
        _repo = repo;
        _taskTypeRepo = taskTypeRepo;
        _employeeRepo = employeeRepo;
        _uow = uow;
    }

    public async Task<int> Handle(CreateTaskCommand command, CancellationToken ct)
    {
        TaskType? Type = await _taskTypeRepo.GetByIdAsync(command.TypeId, ct);
        if (Type is null)
            throw new InvalidOperationException($"Task type with id {command.TypeId} does not exist");

        Employee? employee = await _employeeRepo.GetByIdAsync(command.EmployeeId, ct);
        if (employee is null)
            throw new InvalidOperationException($"Employee with id {command.EmployeeId} does not exist");

        if (!employee.IsActive)
            throw new InvalidOperationException($"Cannot assign task to inactive employee ");

        var item = new EmployeeTask()
        {
            Type = Type,
            EmployeeAssigned = employee,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
        };
        await _repo.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}
