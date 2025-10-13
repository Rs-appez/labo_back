using ParcBack.Application.Abstractions;
using ParcBack.Application.Zones;
using ParcBack.Application.Zones.CreateZone;
using ParcBack.Application.Zones.GetZoneById;
using ParcBack.Application.Zones.ListZones;
using ParcBack.Application.Zones.DeleteZone;
using Microsoft.AspNetCore.Mvc;

namespace ParcBack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZonesController : ControllerBase
{
    private readonly ICommandHandler<CreateZoneCommand, int> _create;
    private readonly IQueryHandler<GetZoneByIdQuery, ZoneDto?> _getById;
    private readonly IQueryHandler<ListZonesQuery, IReadOnlyList<ZoneDto>> _list;
    private readonly ICommandHandler<DeleteZoneCommand, int> _remove;

    public ZonesController(
        ICommandHandler<CreateZoneCommand, int> create,
        ICommandHandler<DeleteZoneCommand, int> remove,
        IQueryHandler<GetZoneByIdQuery, ZoneDto?> getById,
        IQueryHandler<ListZonesQuery, IReadOnlyList<ZoneDto>> list)
    {
        _create = create;
        _remove = remove;
        _getById = getById;
        _list = list;
    }

    public record CreateZoneRequest(string Name);

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateZoneRequest body, CancellationToken ct)
    {
        int id;
        try
        {
            id = await _create.Handle(new CreateZoneCommand(body.Name), ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ZoneDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _getById.Handle(new GetZoneByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ZoneDto>>> List(CancellationToken ct)
    {
        var dtos = await _list.Handle(new ListZonesQuery(), ct);
        return Ok(dtos);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id, CancellationToken ct)
    {
        try
        {

            var deletedId = await _remove.Handle(new DeleteZoneCommand(id), ct);
            return Ok(deletedId);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
