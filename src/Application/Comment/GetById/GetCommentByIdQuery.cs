using MediatR;

namespace ParcBack.Application.Comments.GetCommentById;

public record GetCommentByIdQuery(
    int Id
) : IRequest<CommentDto?>;
