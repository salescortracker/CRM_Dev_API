using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class WorkflowRuleCondition
{
    public int WorkflowRuleConditionId { get; set; }

    public int WorkflowRuleId { get; set; }

    public string FieldName { get; set; } = null!;

    public string Operator { get; set; } = null!;

    public string? FieldValue { get; set; }

    public string? LogicalOperator { get; set; }

    public int ConditionOrder { get; set; }

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
