using MediatR;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.TaskTypes.GetTaskTypeById;

public class GetTaskTypeByIdHandler : IRequestHandler<GetTaskTypeByIdQuery, TaskTypeDto?>
{
    private readonly ITaskTypeRepository _repo;

    public GetTaskTypeByIdHandler(ITaskTypeRepository repo) => _repo = repo;

    public async Task<TaskTypeDto?> Handle(GetTaskTypeByIdQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(query.Id, ct);
        return item?.ToDto();
    }
}
