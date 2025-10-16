using ParcBack.Application.Employees;
using ParcBack.Application.Employees.Register;
// using ParcBack.Application.Employees.GetEmployeeById;
// using ParcBack.Application.Employees.ListEmployees;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ParcBack.Application.Employees.Login;
using ParcBack.Domain.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace ParcBack.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public EmployeesController(IMediator mediator, ITokenService tokenService)
    {
        _mediator = mediator;
        _tokenService = tokenService;
    }


    public record RegisterRequest(string Email, string Password);

    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] RegisterRequest body, CancellationToken ct)
    {
        try
        {
            EmployeeDto employee = await _mediator.Send(new LoginQuery(body.Email, body.Password), ct);
            if (employee == null) return Unauthorized("Invalid email or password");
            var token  =_tokenService.GenerateToken(employee);
            return Ok(new { Employee = employee, Token = token });

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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


