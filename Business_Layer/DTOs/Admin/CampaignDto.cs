using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class CampaignDto
    {
        public int CampaignId { get; set; }

        public int? CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public string CampaignName { get; set; } = string.Empty;

        public string CampaignType { get; set; } = string.Empty;

        public int? MarketingListId { get; set; }

        public int TotalRecipients { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
