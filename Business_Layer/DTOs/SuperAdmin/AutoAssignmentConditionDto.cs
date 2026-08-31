using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class AutoAssignmentConditionDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int AutoAssignmentConditionId { get; set; }


        // =====================================================
        // AUTO ASSIGNMENT RULE
        // =====================================================

        public int AutoAssignmentRuleId { get; set; }


        // =====================================================
        // CONDITION INFORMATION
        // =====================================================

        public string FieldName { get; set; } = string.Empty;

        public string Operator { get; set; } = string.Empty;

        public string? FieldValue { get; set; }

        public string? LogicalOperator { get; set; }

        public int ConditionOrder { get; set; }


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