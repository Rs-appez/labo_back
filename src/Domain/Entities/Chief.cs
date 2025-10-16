namespace ParcBack.Domain.Entities;

public class Chief : Employee
{
    public List<Employee> Employees { get; set; } = new();
}
