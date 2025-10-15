using ParcBack.Domain.Repositories;
using MediatR;

namespace ParcBack.Application.Rides.GetRideById;

public class GetRideByIdHandler : IRequestHandler<GetRideByIdQuery, RideDto?>
{
    private readonly IRideRepository _repo;

    public GetRideByIdHandler(IRideRepository repo) => _repo = repo;

    public async Task<RideDto?> Handle(GetRideByIdQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(query.Id, ct);
        Console.WriteLine("item : " + item);

        return item?.ToDto();
    }
}

