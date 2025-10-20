using System.ComponentModel.DataAnnotations;

namespace ParcBack.Domain.Entities;

public class TaskType
{
    public int Id { get; set; }
    [Required, MaxLength(100), MinLength(3)]
    public required string Name { get; set; }
}

