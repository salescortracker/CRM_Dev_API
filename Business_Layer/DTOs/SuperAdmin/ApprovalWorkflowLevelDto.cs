using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class ApprovalWorkflowLevelDto
    {
        public int ApprovalWorkflowLevelId { get; set; }

        // =====================================================
        // APPROVAL WORKFLOW
        // =====================================================

        public int ApprovalWorkflowId { get; set; }

        public string? WorkflowName { get; set; }

        // =====================================================
        // LEVEL INFORMATION
        // =====================================================

        public int LevelNumber { get; set; }

        public string ApproverType { get; set; } = string.Empty;

        public int? ApproverUserId { get; set; }

        public string? ApproverUserName { get; set; }

        public int? ApproverRoleId { get; set; }

        public string? ApproverRoleName { get; set; }

        public string? ApprovalCondition { get; set; }

        public string? OnApprovalAction { get; set; }

        public string? OnRejectionAction { get; set; }

        // =====================================================
        // COMPANY / REGION
        // =====================================================

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        // =====================================================
        // AUDIT
        // =====================================================

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
