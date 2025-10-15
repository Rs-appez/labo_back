using MediatR;
namespace ParcBack.Application.Rides.GetRideById;

public record GetRideByIdQuery(int Id) : IRequest<RideDto?>;
