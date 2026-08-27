using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CommunicationNotificationTemplateDto
    {
        public int NotificationTemplateId { get; set; }

        public string TemplateCode { get; set; } = string.Empty;

        public string TemplateName { get; set; } = string.Empty;

        public string? NotificationType { get; set; }

        public string? Category { get; set; }

        public string? Title { get; set; }

        public string MessageBody { get; set; } = string.Empty;

        public string? Channel { get; set; }

        public string? LanguageCode { get; set; }

        public int Version { get; set; } = 1;

        public string Status { get; set; } = "Draft";

        public bool IsActive { get; set; } = true;
    }
}
