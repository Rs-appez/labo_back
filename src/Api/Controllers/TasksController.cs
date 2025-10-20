using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ParcBack.Domain.Tokens;
using ParcBack.Application.EmployeeTasks.GetTaskById;
using ParcBack.Application.EmployeeTasks.CreateTask;
using ParcBack.Application.EmployeeTasks;

namespace ParcBack.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public TasksController(IMediator mediator, ITokenService tokenService)
    {
        _mediator = mediator;
        _tokenService = tokenService;
    }


    public record CreateTaskRequest(int TaskTypeId, Guid? AssignedEmployeeId, DateTime start, DateTime end);

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateTaskRequest body, CancellationToken ct)
    {
        if (!_tokenService.IsChiefToken(User))
            return StatusCode(403, "Only chief can create task.");

        int id;
        try
        {
            id = await _mediator.Send(new CreateTaskCommand(
                        body.TaskTypeId, body.AssignedEmployeeId, body.start, body.end), ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeTaskDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetTaskByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

}
