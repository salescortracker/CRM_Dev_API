using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class EmailAutomationDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int EmailAutomationId { get; set; }


        // =====================================================
        // EMAIL AUTOMATION INFORMATION
        // =====================================================

        public string AutomationName { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string TriggerEvent { get; set; } = string.Empty;

        public int? EmailTemplateId { get; set; }

        public string RecipientType { get; set; } = string.Empty;

        public string ScheduleType { get; set; } = string.Empty;

        public int? DelayMinutes { get; set; }

        public string? FromEmail { get; set; }


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