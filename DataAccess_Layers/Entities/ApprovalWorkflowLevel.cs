using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ApprovalWorkflowLevel
{
    public int ApprovalWorkflowLevelId { get; set; }

    public int ApprovalWorkflowId { get; set; }

    public int LevelNumber { get; set; }

    public string ApproverType { get; set; } = null!;

    public int? ApproverUserId { get; set; }

    public int? ApproverRoleId { get; set; }

    public string? ApprovalCondition { get; set; }

    public string? OnApprovalAction { get; set; }

    public string? OnRejectionAction { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ApprovalWorkflow ApprovalWorkflow { get; set; } = null!;
}
