using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class AuditLog
{
    public int AuditId { get; set; }

    public string? TableName { get; set; }

    public string? ActionType { get; set; }

    public int? RecordId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }
}
