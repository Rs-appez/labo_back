using MediatR;
namespace ParcBack.Application.Zones.ListZones;

public record ListZonesQuery() : IRequest<IReadOnlyList<ZoneDto>>;
