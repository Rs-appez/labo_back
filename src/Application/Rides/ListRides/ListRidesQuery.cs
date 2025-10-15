using MediatR;
namespace ParcBack.Application.Rides.ListRides;

public record ListRidesQuery() : IRequest<IReadOnlyList<RideDto>>;
