using MediatR;
namespace ParcBack.Application.Roles.GetRoleByName;

public record GetRoleByNameQuery(string name) : IRequest<RoleDto?>;
