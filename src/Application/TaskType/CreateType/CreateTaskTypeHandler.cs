using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.TaskTypes.CreateTaskType;

public class CreateTaskTypeHandler : IRequestHandler<CreateTaskTypeCommand, int>
{
    private readonly ITaskTypeRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateTaskTypeHandler(ITaskTypeRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<int> Handle(CreateTaskTypeCommand command, CancellationToken ct)
    {
        if (await _repo.GetByNameAsync(command.Name, ct) is not null)
        {
            throw new InvalidOperationException($"TaskType with name {command.Name} already exists");
        }
        var item = new TaskType()
        {
            Name = command.Name
        };
        await _repo.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}
