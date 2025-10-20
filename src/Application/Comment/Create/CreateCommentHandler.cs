using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;

namespace ParcBack.Application.Comments.CreateComment;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, int>
{
    private readonly ICommentRepository _repo;
    private readonly ITaskRepository _taskRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IUnitOfWork _uow;

    public CreateCommentHandler(ICommentRepository repo, IUnitOfWork uow,
            ITaskRepository taskRepo, IEmployeeRepository employeeRepo)
    {
        _repo = repo;
        _taskRepo = taskRepo;
        _employeeRepo = employeeRepo;
        _uow = uow;
    }

    public async Task<int> Handle(CreateCommentCommand command, CancellationToken ct)
    {
        EmployeeTask? task = await _taskRepo.GetByIdAsync(command.TaskId, ct);
        if (task is null)
            throw new InvalidOperationException($"Task with id {command.TaskId} does not exist");

        Employee? employee = await _employeeRepo.GetByIdAsync(command.AuthorId, ct);
        if (employee is null)
            throw new InvalidOperationException($"Employee with id {command.AuthorId} does not exist");

        if (!employee.IsActive)
            throw new InvalidOperationException($"Employee with id {command.AuthorId} is not active");

        var item = new Comment()
        {
            Content = command.Content,
            Employee = employee,
            Task = task
        };
        await _repo.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}
