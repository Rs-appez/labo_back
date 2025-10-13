using ParcBack.Application.Abstractions;
using ParcBack.Application.Zones;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Zones.GetZoneByTheme;

public class GetZoneByThemeHandler : IQueryHandler<GetZoneByThemeQuery, ZoneDto?>
{
    private readonly IZoneRepository _repo;

    public GetZoneByThemeHandler(IZoneRepository repo) => _repo = repo;

    public async Task<ZoneDto?> Handle(GetZoneByThemeQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByThemeAsync(query.Name, ct);
        return item?.ToDto();
    }
}
