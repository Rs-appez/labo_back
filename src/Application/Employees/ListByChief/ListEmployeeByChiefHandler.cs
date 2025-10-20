using MediatR;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Employees.ListEmployeesByChief;

public class ListEmployeesByChiefHandler : IRequestHandler<ListEmployeesByChiefQuery, IReadOnlyList<EmployeeDto>>
{
    private readonly IEmployeeRepository _repo;

    public ListEmployeesByChiefHandler(IEmployeeRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<EmployeeDto>> Handle(ListEmployeesByChiefQuery query, CancellationToken ct)
    {
        var items = await _repo.ListAsync(query.chiefId, ct);
        return items.Select(i => i.ToDto()).ToList();
    }
}
