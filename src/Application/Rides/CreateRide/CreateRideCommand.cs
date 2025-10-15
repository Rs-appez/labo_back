using MediatR;
namespace ParcBack.Application.Rides.CreateRide;

public record CreateRideCommand(string Name, int ZoneId) : IRequest<int>;
