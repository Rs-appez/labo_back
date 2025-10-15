using System.ComponentModel.DataAnnotations;
using ParcBack.Application.Zones;
namespace ParcBack.Application.Rides;

public record RideDto(
    int Id,
    [Required, MaxLength(100), MinLength(3)]
    string Name,
    [Required]
    ZoneDto Zone
);

