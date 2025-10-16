using System.ComponentModel.DataAnnotations;

namespace ParcBack.Domain.Entities;

public class Employee
{
    public Guid Id { get; private set; }

    [Required, MaxLength(100), MinLength(5), EmailAddress]
    public required string Email { get; set; }

    [Required, MaxLength(200), MinLength(8)]
    public required string PasswordHash { get; set; }

    public bool IsActive { get; set; } = true;
    public Role Role { get; set; } 

    public Guid? ChiefId { get; set; }

    public DateTime CreatedAt
    { get; private set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public Employee() { } // Parameterless constructor for EF Core

    public override string ToString() => $"Employee {{ Id = {Id}, Email = {Email}, IsActive = {IsActive}, CreatedAt = {CreatedAt}, LastLoginAt = {LastLoginAt} }}";
}

