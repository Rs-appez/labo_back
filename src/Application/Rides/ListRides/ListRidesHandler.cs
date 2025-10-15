using MediatR;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Rides.ListRides;

public class ListRidesHandler : IRequestHandler<ListRidesQuery, IReadOnlyList<RideDto>>
{
    private readonly IRideRepository _repo;

    public ListRidesHandler(IRideRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<RideDto>> Handle(ListRidesQuery query, CancellationToken ct)
    {
        var items = await _repo.ListAsync(ct);
        return [.. items.Select(i => i.ToDto())];
    }
}

