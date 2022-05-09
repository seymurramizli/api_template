using System;
using System.Collections.Generic;

namespace ApiTemplate.Entities
{
    public partial class Audit
    {
        public Audit()
        {
            AuditDetails = new HashSet<AuditDetail>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TableName { get; set; } = null!;
        public byte AuditType { get; set; }
        public string? UserName { get; set; }
        public int TableId { get; set; }

        public virtual ICollection<AuditDetail> AuditDetails { get; set; }
    }
}
