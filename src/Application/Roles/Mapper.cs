using ParcBack.Domain.Entities;

namespace ParcBack.Application.Roles;

public static class Mappers
{
    public static RoleDto ToDto(this Role ride) =>
        new RoleDto(
            ride.Id,
            ride.Name
        );
}
