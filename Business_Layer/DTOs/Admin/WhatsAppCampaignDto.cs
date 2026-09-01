using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class WhatsAppCampaignDto
    {

        public int WhatsAppCampaignId { get; set; }

        public int? CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public string CampaignName { get; set; } = string.Empty;

        public int? MarketingListId { get; set; }

        public int? WhatsAppTemplateId { get; set; }

        public string? Language { get; set; }

        public string? Message { get; set; }

        public string? MediaUrl { get; set; }

        public string? MediaType { get; set; }

        public int TotalRecipients { get; set; }

        public int SentCount { get; set; }

        public int DeliveredCount { get; set; }

        public int ReadCount { get; set; }

        public int RepliedCount { get; set; }

        public int FailedCount { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
