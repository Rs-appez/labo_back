using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.EmployeeTasks.CreateTask;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, int>
{
    private readonly ITaskRepository _repo;
    private readonly ITaskTypeRepository _taskTypeRepo;
    private readonly IUnitOfWork _uow;

    public CreateTaskHandler(ITaskRepository repo, IUnitOfWork uow, ITaskTypeRepository taskTypeRepo)
    {
        _repo = repo;
        _taskTypeRepo = taskTypeRepo;
        _uow = uow;
    }

    public async Task<int> Handle(CreateTaskCommand command, CancellationToken ct)
    {
        TaskType? Type = await _taskTypeRepo.GetByIdAsync(command.TypeId, ct);
        if (Type is null)
            throw new InvalidOperationException($"Task type with id {command.TypeId} does not exist");

        var item = new EmployeeTask()
        {
            Type = Type,
        };
        await _repo.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}
