using ParcBack.Application.Zones;
using ParcBack.Application.Zones.CreateZone;
using ParcBack.Application.Zones.GetZoneById;
using ParcBack.Application.Zones.ListZones;
using ParcBack.Application.Zones.DeleteZone;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ParcBack.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ZonesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ZonesController(IMediator mediator) { _mediator = mediator; }


    public record CreateZoneRequest(string Theme);

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateZoneRequest body, CancellationToken ct)
    {
        int id;
        try
        {
            id = await _mediator.Send(new CreateZoneCommand(body.Theme), ct);
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
        var dto = await _mediator.Send(new GetZoneByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ZoneDto>>> List(CancellationToken ct)
    {
        var dtos = await _mediator.Send(new ListZonesQuery(), ct);
        return Ok(dtos);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Delete(int id, CancellationToken ct)
    {
        try
        {
            var deletedId = await _mediator.Send(new DeleteZoneCommand(id), ct);
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
