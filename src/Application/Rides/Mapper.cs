using ParcBack.Domain.Entities;
using ParcBack.Application.Zones;

namespace ParcBack.Application.Rides;

public static class Mappers
{
    public static RideDto ToDto(this Ride ride) =>
        new RideDto(
            ride.Id,
            ride.Name,
            ride.Zone.ToDto()
        );
}

