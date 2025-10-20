using MediatR;
namespace ParcBack.Application.Employees.ListEmployeesByChief;

public record ListEmployeesByChiefQuery(Guid chiefId) : IRequest<IReadOnlyList<EmployeeDto>>;
