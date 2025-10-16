using MediatR;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Employees.ListAllEmployees;

public class ListAllEmployeesHandler : IRequestHandler<ListAllEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    private readonly IEmployeeRepository _repo;

    public ListAllEmployeesHandler(IEmployeeRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<EmployeeDto>> Handle(ListAllEmployeesQuery query, CancellationToken ct)
    {
        var items = await _repo.ListAsync(ct: ct);
        return items.Select(i => i.ToDto()).ToList();
    }
}
