using ParcBack.Application.Zones;
namespace ParcBack.Application.Rides;

public record RideDto(
    int Id,
    string Name,
    ZoneDto Zone
);

