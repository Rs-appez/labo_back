using MediatR;
namespace ParcBack.Application.Zones.DeleteZone;

public record DeleteZoneCommand(int Id) : IRequest<int>;
