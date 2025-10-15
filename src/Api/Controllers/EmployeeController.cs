using ParcBack.Application.Employees;
using ParcBack.Application.Employees.Register;
// using ParcBack.Application.Employees.GetEmployeeById;
// using ParcBack.Application.Employees.ListEmployees;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace ParcBack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator) { _mediator = mediator; }


    public record RegisterRequest(string Email, string Password);

    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterRequest body, CancellationToken ct)
    {
        Guid id;
        try
        {
            id = await _mediator.Send(new RegisterCommand(body.Email, body.Password), ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
        // var dto = await _mediator.Send(new GetEmployeeByIdQuery(id), ct);
        // return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> List(CancellationToken ct)
    {
        throw new NotImplementedException();
        // var dtos = await _mediator.Send(new ListEmployeesQuery(), ct);
        // return Ok(dtos);
    }
    //
    // [HttpDelete("{id:int}")]
    // public async Task<ActionResult<int>> Delete(int id, CancellationToken ct)
    // {
    //     try
    //     {
    //         var deletedId = await _mediator.Send(new DeleteEmployeeCommand(id), ct);
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


