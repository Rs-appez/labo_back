using ParcBack.Application.Rides;
using ParcBack.Application.Rides.CreateRide;
using ParcBack.Application.Rides.GetRideById;
using ParcBack.Application.Rides.ListRides;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ParcBack.Domain.Tokens;

namespace ParcBack.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RidesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public RidesController(IMediator mediator, ITokenService tokenService)
    {
        _mediator = mediator;
        _tokenService = tokenService;
    }


    public record CreateRideRequest(string Name, int ZoneId);

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateRideRequest body, CancellationToken ct)
    {
        if (!_tokenService.IsAdminToken(User))
            return StatusCode(403, "Only admins can create rides.");

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

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RideDto>>> List(CancellationToken ct)
    {
        var dtos = await _mediator.Send(new ListRidesQuery(), ct);
        return Ok(dtos);
    }

    // [HttpDelete("{id:int}")]
    // public async Task<ActionResult<int>> Delete(int id, CancellationToken ct)
    // {
    //     if (!_tokenService.IsAdminToken(User))
                // return StatusCode(403, "Only admins can delete zones.");
                //
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

