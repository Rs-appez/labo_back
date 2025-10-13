using MediatR;
namespace ParcBack.Application.Zones.GetZoneById;

public record GetZoneByIdQuery(int Id) : IRequest<ZoneDto?>;
