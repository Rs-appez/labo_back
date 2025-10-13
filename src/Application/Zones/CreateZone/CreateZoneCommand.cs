using MediatR;
namespace ParcBack.Application.Zones.CreateZone;

public record CreateZoneCommand(string Theme) : IRequest<int>;
