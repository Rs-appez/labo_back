using MediatR;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Roles.GetRoleByName;

public class GetRoleByNameHandler : IRequestHandler<GetRoleByNameQuery, RoleDto?>
{
    private readonly IRoleRepository _repo;

    public GetRoleByNameHandler(IRoleRepository repo) => _repo = repo;

    public async Task<RoleDto?> Handle(GetRoleByNameQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByNameAsync(query.name, ct);
        return item?.ToDto();
    }
}
