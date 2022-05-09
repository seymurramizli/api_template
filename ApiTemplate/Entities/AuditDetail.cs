using System;
using System.Collections.Generic;

namespace ApiTemplate.Entities
{
    public partial class AuditDetail
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuditId { get; set; }
        public string ColumnName { get; set; } = null!;
        public string? OldValue { get; set; }
        public string NewValue { get; set; } = null!;

        public virtual Audit Audit { get; set; } = null!;
    }
}
