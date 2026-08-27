using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CommunicationSMSTemplateDto
    {
        public int SmstemplateId { get; set; }

        public string TemplateCode { get; set; } = string.Empty;

        public string TemplateName { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string MessageBody { get; set; } = string.Empty;

        public int? CharacterCount { get; set; }

        public string? LanguageCode { get; set; }

        public string? ProviderName { get; set; }

        public int Version { get; set; } = 1;

        public string Status { get; set; } = "Draft";

        public bool IsActive { get; set; } = true;
    }
}
