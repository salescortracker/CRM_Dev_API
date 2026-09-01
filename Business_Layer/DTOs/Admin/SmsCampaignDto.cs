using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class SmsCampaignDto
    {

        public int SmscampaignId { get; set; }

        public int? CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public string CampaignName { get; set; } = string.Empty;

        public int? MarketingListId { get; set; }

        public int? SmstemplateId { get; set; }

        public string? Sender { get; set; }

        public string? Message { get; set; }

        public int TotalRecipients { get; set; }

        public int SentCount { get; set; }

        public int DeliveredCount { get; set; }

        public int FailedCount { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
