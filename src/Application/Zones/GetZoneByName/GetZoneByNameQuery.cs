using MediatR;
namespace ParcBack.Application.Zones.GetZoneByTheme;

public record GetZoneByThemeQuery(string Name) : IRequest<ZoneDto?>;
