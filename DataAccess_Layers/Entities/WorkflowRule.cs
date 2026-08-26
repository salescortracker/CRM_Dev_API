using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class WorkflowRule
{
    public int WorkflowRuleId { get; set; }

    public string WorkflowRuleName { get; set; } = null!;

    public string WorkflowRuleCode { get; set; } = null!;

    public string? Description { get; set; }

    public string ModuleName { get; set; } = null!;

    public string TriggerEvent { get; set; } = null!;

    public string ExecutionType { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ExecutionOrder { get; set; }

    public bool StopProcessing { get; set; }

    public virtual ICollection<WorkflowRuleAction> WorkflowRuleActions { get; set; } = new List<WorkflowRuleAction>();

    public virtual ICollection<WorkflowRuleCondition> WorkflowRuleConditions { get; set; } = new List<WorkflowRuleCondition>();
}
