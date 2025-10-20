using System.ComponentModel.DataAnnotations;
namespace ParcBack.Application.TaskTypes;

public record TaskTypeDto(
    int Id,
    [Required, MaxLength(100), MinLength(3)]
    string Name
);
