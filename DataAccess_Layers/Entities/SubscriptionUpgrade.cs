using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class SubscriptionUpgrade
{
    public Guid SubscriptionUpgradeId { get; set; }

    public Guid SubscriptionId { get; set; }

    public Guid PreviousPlanId { get; set; }

    public Guid NewPlanId { get; set; }

    public DateOnly UpgradeDate { get; set; }

    public string? Reason { get; set; }

    public decimal ProratedAmount { get; set; }

    public Guid? InvoiceId { get; set; }

    public byte Status { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Invoice? Invoice { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual SubscriptionPlan NewPlan { get; set; } = null!;

    public virtual SubscriptionPlan PreviousPlan { get; set; } = null!;

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
