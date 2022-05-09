using System;
using System.Collections.Generic;

namespace ApiTemplate.Entities
{
    public partial class Employee
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte Status { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
    }
}
