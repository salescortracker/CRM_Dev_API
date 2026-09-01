using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Campaign
{
    public int CampaignId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public string CampaignName { get; set; } = null!;

    public string CampaignType { get; set; } = null!;

    public int? MarketingListId { get; set; }

    public int TotalRecipients { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
