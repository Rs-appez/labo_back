using System.ComponentModel.DataAnnotations;

namespace ParcBack.Domain.Entities;

public class Ride
{
    public int Id { get; private set; }

    [Required, MaxLength(100), MinLength(3)]
    public required string Name { get; set; }
    [Required]
    public required Zone Zone { get; set; }

    public Ride() { } // Parameterless constructor for EF Core

    public Ride(string name, Zone zone)
    {
        Name = name;
        Zone = zone;
    }

    public override string ToString() => $"Ride {{ Id = {Id}, Name = {Name}, Zone = {Zone} }}";

}
