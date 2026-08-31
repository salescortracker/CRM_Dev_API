using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class EscalationRuleDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int EscalationRuleId { get; set; }


        // =====================================================
        // ESCALATION RULE INFORMATION
        // =====================================================

        public string RuleName { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int EscalateAfterMinutes { get; set; }

        public int EscalationLevel { get; set; }

        public string EscalateToType { get; set; } = string.Empty;

        public int? EscalateToUserId { get; set; }

        public string NotificationMethod { get; set; } = string.Empty;

        public bool RepeatEscalation { get; set; }

        public int? MaximumEscalationLevel { get; set; }


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