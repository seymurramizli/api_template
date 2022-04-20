namespace ApiTemplate.Entities;

public class Department
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Status { get; set; }
    public string? Name { get; set; }

    public ICollection<Employee> Employees { get; set; }
}