using ParcBack.Domain.Repositories;
using MediatR;

namespace ParcBack.Application.Employees.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IEmployeeRepository _repo;

    public GetEmployeeByIdHandler(IEmployeeRepository repo) => _repo = repo;

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(query.Id, ct);
        Console.WriteLine("item : " + string.Join(",",item!.Tasks));

        return item?.ToDto();
    }
}
