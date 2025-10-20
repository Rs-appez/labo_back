using ParcBack.Domain.Entities;

namespace ParcBack.Application.Comments;

public static class Mappers
{
    public static CommentDto ToDto(this Comment comment) =>
        new (
            comment.Id,
            comment.Content,
            comment.Employee.Id,
            comment.Task.Id,
            comment.CreatedAt
        );
}
