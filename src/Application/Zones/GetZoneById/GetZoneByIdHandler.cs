using ParcBack.Application.Abstractions;
using ParcBack.Application.Zones;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Zones.GetZoneById;

public class GetZoneByIdHandler : IQueryHandler<GetZoneByIdQuery, ZoneDto?>
{
    private readonly IZoneRepository _repo;

    public GetZoneByIdHandler(IZoneRepository repo) => _repo = repo;

    public async Task<ZoneDto?> Handle(GetZoneByIdQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(query.Id, ct);
        return item?.ToDto();
    }
}
