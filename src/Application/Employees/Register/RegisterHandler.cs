using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.Employees.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IEmployeeRepository _repo;
    private readonly IUnitOfWork _uow;

    public RegisterHandler(IEmployeeRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(RegisterCommand command, CancellationToken ct)
    {
        Employee item = new()
        {
            Email = command.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
        };
        await _repo.Register(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}

