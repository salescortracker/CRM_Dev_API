using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class EmailAutomationRecipientDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int EmailAutomationRecipientId { get; set; }


        // =====================================================
        // EMAIL AUTOMATION RECIPIENT INFORMATION
        // =====================================================

        public int EmailAutomationId { get; set; }

        public string RecipientType { get; set; } = string.Empty;

        public string RecipientValue { get; set; } = string.Empty;


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