using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.Employees.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IEmployeeRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IRoleRepository _repoRole;

    public RegisterHandler(IEmployeeRepository repo, IRoleRepository repo_role, IUnitOfWork uow)
    {
        _repo = repo;
        _repoRole = repo_role;
        _uow = uow;
    }

    public async Task<Guid> Handle(RegisterCommand command, CancellationToken ct)
    {
        Role? role = await _repoRole.GetByNameAsync(command.Role, ct);
        if (role is null)
            throw new ArgumentException($"Role with name {command.Role} does not exist");

        Employee item = new()
        {
            Email = command.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
            Role = role,
        };
        await _repo.Register(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}
