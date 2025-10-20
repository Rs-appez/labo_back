using ParcBack.Application.Employees;
using ParcBack.Application.Employees.Register;
using ParcBack.Application.Employees.GetEmployeeById;
using ParcBack.Application.Employees.ListAllEmployees;
using ParcBack.Application.Employees.ListEmployeesByChief;
using ParcBack.Application.Employees.Desactivate;
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
            var token = _tokenService.GenerateToken(employee);
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
        var dto = await _mediator.Send(new GetEmployeeByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> List(CancellationToken ct)
    {
        if (_tokenService.IsAdminToken(User))
        {
            var allEmployees = await _mediator.Send(new ListAllEmployeesQuery(), ct);
            return Ok(allEmployees);
        }
        if (!_tokenService.IsChiefToken(User))
            return StatusCode(403, "Only chiefs can list employees.");

        var dtos = await _mediator.Send(new ListEmployeesByChiefQuery(_tokenService.GetUserId(User)), ct);
        return Ok(dtos);
    }
    [HttpPost("{id:guid}/desactivate")]
    public async Task<ActionResult> Desactivate(Guid id, CancellationToken ct)
    {
        try
        {
            await _mediator.Send(new DesactivateEmployeeCommand(id), ct);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
