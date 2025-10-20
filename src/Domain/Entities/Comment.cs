using System.ComponentModel.DataAnnotations;

namespace ParcBack.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    [Required, MaxLength(1000), MinLength(1)]
    public required string Content { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public required Employee Employee { get; set; }
    public required EmployeeTask Task { get; set; }
}
