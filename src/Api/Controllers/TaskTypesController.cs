using ParcBack.Application.TaskTypes;
using ParcBack.Application.TaskTypes.CreateTaskType;
using ParcBack.Application.TaskTypes.GetTaskTypeByName;
using ParcBack.Application.TaskTypes.GetTaskTypeById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ParcBack.Domain.Tokens;

namespace ParcBack.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskTypesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public TaskTypesController(IMediator mediator, ITokenService tokenService)
    {
        _mediator = mediator;
        _tokenService = tokenService;
    }


    public record CreateTaskTypeRequest(string Name);

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateTaskTypeRequest body, CancellationToken ct)
    {
        if (!_tokenService.IsAdminToken(User))
            return StatusCode(403, "Only admins can create task type.");

        int id;
        try
        {
            id = await _mediator.Send(new CreateTaskTypeCommand(body.Name), ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<TaskTypeDto>> GetByName(string name, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetTaskTypeByNameQuery(name), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskTypeDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetTaskTypeByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

}
