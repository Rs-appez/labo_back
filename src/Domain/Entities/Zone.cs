using System.ComponentModel.DataAnnotations;

namespace ParcBack.Domain.Entities;

public class Zone
{
    public int Id { get; private set; }
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public required string Theme { get; set; }

    public Zone() { } // Parameterless constructor for EF Core

    public Zone(string theme)
    {
        Theme = theme;
    }
}
