namespace ParcBack.Application.Comments;

public record CommentDto(
    int Id,
    string Content,
    Guid AuthorId,
    int TaskId,
    DateTime CreatedAt
);
