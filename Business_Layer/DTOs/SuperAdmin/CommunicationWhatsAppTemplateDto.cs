using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CommunicationWhatsAppTemplateDto
    {
        public int WhatsAppTemplateId { get; set; }

        public string TemplateCode { get; set; } = string.Empty;

        public string TemplateName { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string? LanguageCode { get; set; }

        public string ProviderName { get; set; } = "Twilio";

        public string? TemplateSid { get; set; }

        public string? HeaderText { get; set; }

        public string BodyText { get; set; } = string.Empty;

        public string? FooterText { get; set; }

        public string ApprovalStatus { get; set; } = "Pending";

        public string? RejectionReason { get; set; }

        public int Version { get; set; } = 1;

        public string Status { get; set; } = "Draft";

        public bool IsActive { get; set; } = true;
    }
}
