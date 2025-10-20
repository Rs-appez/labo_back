using ParcBack.Application.Comments;
using ParcBack.Application.Comments.CreateComment;
using ParcBack.Application.Comments.GetCommentById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ParcBack.Domain.Tokens;

namespace ParcBack.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public CommentsController(IMediator mediator, ITokenService tokenService)
    {
        _mediator = mediator;
        _tokenService = tokenService;
    }


    public record CreateCommentRequest(string content, int taskId, Guid authorId);

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateCommentRequest body, CancellationToken ct)
    {
        if (!_tokenService.IsAdminToken(User))
            return StatusCode(403, "Only admins can create rides.");

        int id;
        try
        {
            id = await _mediator.Send(new CreateCommentCommand(
                body.content,
                body.taskId,
                body.authorId
            ), ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetCommentByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }
}
