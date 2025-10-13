using MediatR;
using ParcBack.Application.Zones;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Zones.ListZones;

public class ListZonesHandler : IRequestHandler<ListZonesQuery, IReadOnlyList<ZoneDto>>
{
    private readonly IZoneRepository _repo;

    public ListZonesHandler(IZoneRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<ZoneDto>> Handle(ListZonesQuery query, CancellationToken ct)
    {
        var items = await _repo.ListAsync(ct);
        return items.Select(i => i.ToDto()).ToList();
    }
}
