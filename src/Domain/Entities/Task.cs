namespace ParcBack.Domain.Entities;

public class EmployeeTask
{
    public int Id { get; private set; }

    public required TaskType Type { get; set; }

    public bool IsCompleted { get; set; } = false;
    public bool IsValidated { get; set; } = false;

    public EmployeeTask() { } // Parameterless constructor for EF Core

    public override string ToString() => $"Task {{ Id = {Id}, Type = {Type} }}";

}
