using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class ApprovalWorkflowDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int ApprovalWorkflowId { get; set; }


        // =====================================================
        // WORKFLOW INFORMATION
        // =====================================================

        public string WorkflowName { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string ApprovalType { get; set; } = string.Empty;

        public int ApprovalLevels { get; set; }

        public string? FinalApprovalAction { get; set; }

        public string? FinalRejectionAction { get; set; }


        // =====================================================
        // COMPANY / REGION
        // =====================================================

        public int CompanyId { get; set; }

        public int RegionId { get; set; }


        // =====================================================
        // STATUS
        // =====================================================

        public bool IsActive { get; set; }


        // =====================================================
        // AUDIT
        // =====================================================

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
