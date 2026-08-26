using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class WorkflowRuleAction
{
    public int WorkflowRuleActionId { get; set; }

    public int WorkflowRuleId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? ActionName { get; set; }

    public string? ActionConfiguration { get; set; }

    public int ActionOrder { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual WorkflowRule WorkflowRule { get; set; } = null!;
}
