using MediatR;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.TaskTypes.GetTaskTypeByName;

public class GetTaskTypeByNameHandler : IRequestHandler<GetTaskTypeByNameQuery, TaskTypeDto?>
{
    private readonly ITaskTypeRepository _repo;

    public GetTaskTypeByNameHandler(ITaskTypeRepository repo) => _repo = repo;

    public async Task<TaskTypeDto?> Handle(GetTaskTypeByNameQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByNameAsync(query.Name, ct);
        return item?.ToDto();
    }
}
