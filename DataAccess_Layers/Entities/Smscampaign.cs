using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Smscampaign
{
    public int SmscampaignId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public string CampaignName { get; set; } = null!;

    public int? MarketingListId { get; set; }

    public int? SmstemplateId { get; set; }

    public string? Sender { get; set; }

    public string? Message { get; set; }

    public int TotalRecipients { get; set; }

    public int SentCount { get; set; }

    public int DeliveredCount { get; set; }

    public int FailedCount { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
