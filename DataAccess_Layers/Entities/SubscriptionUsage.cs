using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class SubscriptionUsage
{
    public Guid SubscriptionUsageId { get; set; }

    public Guid SubscriptionId { get; set; }

    public DateOnly UsageDate { get; set; }

    public int ActiveUsersCount { get; set; }

    public decimal StorageUsedGb { get; set; }

    public int ApirequestsCount { get; set; }

    public decimal CallingMinutesUsed { get; set; }

    public int SmsusedCount { get; set; }

    public int WhatsAppUsedCount { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
