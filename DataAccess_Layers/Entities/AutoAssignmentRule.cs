using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class AutoAssignmentRule
{
    public int AutoAssignmentRuleId { get; set; }

    public string RuleName { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string? Description { get; set; }

    public string AssignmentMethod { get; set; } = null!;

    public int? TeamId { get; set; }

    public int? UserId { get; set; }

    public int? ExecutionOrder { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<AutoAssignmentCondition> AutoAssignmentConditions { get; set; } = new List<AutoAssignmentCondition>();
}
