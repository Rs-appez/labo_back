using ParcBack.Application.Zones;
namespace ParcBack.Application.Rides;

public record RideDto(
    int Id,
    ZoneDto Zone
);

