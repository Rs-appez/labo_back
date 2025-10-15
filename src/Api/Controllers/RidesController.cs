using ParcBack.Application.Rides;
using ParcBack.Application.Rides.CreateRide;
using ParcBack.Application.Rides.GetRideById;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace ParcBack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RidesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RidesController(IMediator mediator) { _mediator = mediator; }


    public record CreateRideRequest(string Name, int ZoneId);

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateRideRequest body, CancellationToken ct)
    {
        int id;
        try
        {
            id = await _mediator.Send(new CreateRideCommand(body.Name, body.ZoneId), ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RideDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetRideByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    // [HttpGet]
    // public async Task<ActionResult<IReadOnlyList<RideDto>>> List(CancellationToken ct)
    // {
    //     var dtos = await _mediator.Send(new ListRidesQuery(), ct);
    //     return Ok(dtos);
    // }
    //
    // [HttpDelete("{id:int}")]
    // public async Task<ActionResult<int>> Delete(int id, CancellationToken ct)
    // {
    //     try
    //     {
    //         var deletedId = await _mediator.Send(new DeleteRideCommand(id), ct);
    //         return Ok(deletedId);
    //     }
    //     catch (InvalidOperationException ex)
    //     {
    //         return NotFound(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    //
    // }
}

