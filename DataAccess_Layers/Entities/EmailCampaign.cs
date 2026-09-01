using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class EmailCampaign
{
    public int EmailCampaignId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public string CampaignName { get; set; } = null!;

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

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
