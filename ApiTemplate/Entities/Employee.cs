namespace ApiTemplate.Entities;

public class Employee
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Status { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
}