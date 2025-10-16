using System.ComponentModel.DataAnnotations;
using ParcBack.Domain.Entities;
namespace ParcBack.Application.Employees;

public record EmployeeLoginDto(
    Guid Id,

    [Required, MaxLength(100), MinLength(5), EmailAddress]
    string Email,

    [Required, MaxLength(200), MinLength(8)]
    string Password
);


public record EmployeeDto(
    Guid Id,

    [Required, MaxLength(100), MinLength(5), EmailAddress]
    string Email,

    bool IsActive,
    Role Role,

    DateTime CreatedAt,
    DateTime? LastLoginAt
);

