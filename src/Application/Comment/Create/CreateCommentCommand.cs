using MediatR;

namespace ParcBack.Application.Comments.CreateComment;

public record CreateCommentCommand(
    string Content,
    int TaskId,
    Guid AuthorId
) : IRequest<int>;
