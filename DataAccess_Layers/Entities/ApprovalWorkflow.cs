using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ApprovalWorkflow
{
    public int ApprovalWorkflowId { get; set; }

    public string WorkflowName { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string? Description { get; set; }

    public string ApprovalType { get; set; } = null!;

    public int ApprovalLevels { get; set; }

    public string? FinalApprovalAction { get; set; }

    public string? FinalRejectionAction { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<ApprovalWorkflowLevel> ApprovalWorkflowLevels { get; set; } = new List<ApprovalWorkflowLevel>();
}
