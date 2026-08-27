using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CommunicationEmailDto
    {
        public int CommunicationEmailId { get; set; }

        public string ConfigurationName { get; set; } = string.Empty;

        public string ProviderName { get; set; } = string.Empty;

        public string Smtphost { get; set; } = string.Empty;

        public int Smtpport { get; set; }

        public string? Smtpusername { get; set; }

        public string? Smtppassword { get; set; }

        public string FromEmail { get; set; } = string.Empty;

        public string? FromName { get; set; }

        public string? EncryptionType { get; set; }

        public bool EnableAuthentication { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public string? ConnectionStatus { get; set; }

        public DateTime? LastTestedOn { get; set; }
    }
}
