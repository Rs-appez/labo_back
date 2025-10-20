using MediatR;
using ParcBack.Application.EmployeeTasks;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.EmployeeTasks.GetTaskById;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, EmployeeTaskDto?>
{
    private readonly ITaskRepository _repo;

    public GetTaskByIdHandler(ITaskRepository repo) => _repo = repo;

    public async Task<EmployeeTaskDto?> Handle(GetTaskByIdQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(query.Id, ct);
        return item?.ToDto();
    }
}
