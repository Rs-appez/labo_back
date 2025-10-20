using ParcBack.Domain.Repositories;
using MediatR;
using ParcBack.Domain.Abstractions;

namespace ParcBack.Application.Employees.Desactivate;

public class DesactivateEmployeeHandler : IRequestHandler<DesactivateEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public DesactivateEmployeeHandler(IEmployeeRepository repo, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repo = repo;
    }


    public async Task<Unit> Handle(DesactivateEmployeeCommand query, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(query.EmployeeId, ct);
        if (item is null)
            throw new InvalidOperationException($"Employee with id {query.EmployeeId} does not exist");

        item.Deactivate();
        _repo.Update(item);
        await _unitOfWork.SaveChangesAsync(ct);

        return Unit.Value;
    }
}
