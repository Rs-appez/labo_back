using System.ComponentModel.DataAnnotations;

namespace ParcBack.Application.Zones;

public record ZoneDto(
    int Id,
    [Required, MaxLength(100), MinLength(3)]
    string Theme
);
