using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class SlaruleDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int SlaruleId { get; set; }


        // =====================================================
        // SLA INFORMATION
        // =====================================================

        public string Slaname { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Priority { get; set; } = string.Empty;

        public int FirstResponseMinutes { get; set; }

        public int ResolutionMinutes { get; set; }

        public int? WarningMinutes { get; set; }

        public int? BusinessHoursId { get; set; }

        public int? HolidayCalendarId { get; set; }

        public bool EscalationEnabled { get; set; }

        public int? EscalationRuleId { get; set; }


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