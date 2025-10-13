using ParcBack.Application.Abstractions;
using ParcBack.Application.Zones;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Zones.GetZoneById;

public class GetZoneByNameHandler : IQueryHandler<GetZoneByNameQuery, ZoneDto?>
{
    private readonly IZoneRepository _repo;

    public GetZoneByNameHandler(IZoneRepository repo) => _repo = repo;

    public async Task<ZoneDto?> Handle(GetZoneByNameQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByNameAsync(query.Name, ct);
        return item?.ToDto();
    }
}
