using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CommunicationSMSDto
    {
        public int CommunicationSmsid { get; set; }

        public string ConfigurationName { get; set; } = string.Empty;

        public string ProviderName { get; set; } = "Twilio";

        public string AccountSid { get; set; } = string.Empty;

        public string? AuthToken { get; set; }

        public string FromNumber { get; set; } = string.Empty;

        public string? MessagingServiceSid { get; set; }

        public string? WebhookUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public string? ConnectionStatus { get; set; }

        public DateTime? LastTestedOn { get; set; }
    }
}
