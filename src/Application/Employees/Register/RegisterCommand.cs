using MediatR;
namespace ParcBack.Application.Employees.Register;

public record RegisterCommand(string Email, string Password, string Role = "Employee") : IRequest<Guid>;

