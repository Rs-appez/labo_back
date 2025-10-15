using MediatR;
namespace ParcBack.Application.Employees.Register;

public record RegisterCommand(string Email, string Password) : IRequest<Guid>;

