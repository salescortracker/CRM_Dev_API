using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class WhatsAppCampaign
{
    public int WhatsAppCampaignId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public string CampaignName { get; set; } = null!;

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

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
