using ParcBack.Domain.Entities;

namespace ParcBack.Application.Zones;

public static class Mappers
{
    public static ZoneDto ToDto(this Zone zone) =>
        new ZoneDto(
            zone.Id,
            zone.Theme
        );
}
