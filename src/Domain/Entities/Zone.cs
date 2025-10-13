using System.ComponentModel.DataAnnotations;

namespace ParcBack.Domain.Entities;

public class Zone
{
    public int Id { get; private set; }
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Name { get; private set; }

    public Zone() { } // Parameterless constructor for EF Core

    public Zone(string name)
    {
        Name = name;
    }
}
