using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class AutoAssignmentRuleDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int AutoAssignmentRuleId { get; set; }


        // =====================================================
        // RULE INFORMATION
        // =====================================================

        public string RuleName { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string AssignmentMethod { get; set; } = string.Empty;

        public int? TeamId { get; set; }

        public int? UserId { get; set; }

        public int? ExecutionOrder { get; set; }


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