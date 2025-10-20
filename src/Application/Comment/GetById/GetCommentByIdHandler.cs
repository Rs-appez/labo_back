using ParcBack.Domain.Repositories;
using MediatR;

namespace ParcBack.Application.Comments.GetCommentById;

public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly ICommentRepository _repo;

    public GetCommentByIdHandler(ICommentRepository repo) => _repo = repo;

    public async Task<CommentDto?> Handle(GetCommentByIdQuery query, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(query.Id, ct);

        return item?.ToDto();
    }
}
