using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CallingCampaignLead
{
    public Guid CallingCampaignLeadId { get; set; }

    public Guid CallingCampaignId { get; set; }

    public Guid LeadId { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public int QueueOrder { get; set; }

    public byte Status { get; set; }

    public int AttemptCount { get; set; }

    public DateTime? LastAttemptOn { get; set; }

    public DateTime? NextAttemptOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual User? AssignedToUser { get; set; }

    public virtual CallingCampaign CallingCampaign { get; set; } = null!;

    public virtual Lead Lead { get; set; } = null!;
}
