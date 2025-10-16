using System.ComponentModel.DataAnnotations;
namespace ParcBack.Application.Roles;

public record RoleDto(
    int Id,
    [Required, MaxLength(50), MinLength(3)]
    string Name
);

