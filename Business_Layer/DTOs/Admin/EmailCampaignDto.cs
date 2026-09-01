using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class EmailCampaignDto
    {

        public int EmailCampaignId { get; set; }

        public int? CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public string CampaignName { get; set; } = string.Empty;

        public int? MarketingListId { get; set; }

        public int? EmailTemplateId { get; set; }

        public string? Subject { get; set; }

        public string? FromName { get; set; }

        public string? FromEmail { get; set; }

        public string? ReplyToEmail { get; set; }

        public int TotalRecipients { get; set; }

        public int SentCount { get; set; }

        public int DeliveredCount { get; set; }

        public int OpenedCount { get; set; }

        public int ClickedCount { get; set; }

        public int BouncedCount { get; set; }

        public int UnsubscribedCount { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
