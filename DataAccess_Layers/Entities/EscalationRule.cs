using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class EscalationRule
{
    public int EscalationRuleId { get; set; }

    public string RuleName { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string? Description { get; set; }

    public int EscalateAfterMinutes { get; set; }

    public int EscalationLevel { get; set; }

    public string EscalateToType { get; set; } = null!;

    public int? EscalateToUserId { get; set; }

    public string NotificationMethod { get; set; } = null!;

    public bool RepeatEscalation { get; set; }

    public int? MaximumEscalationLevel { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<Slarule> Slarules { get; set; } = new List<Slarule>();
}
