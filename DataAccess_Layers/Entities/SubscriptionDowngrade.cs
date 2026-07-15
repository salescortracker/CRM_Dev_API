using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class SubscriptionDowngrade
{
    public Guid SubscriptionDowngradeId { get; set; }

    public Guid SubscriptionId { get; set; }

    public Guid PreviousPlanId { get; set; }

    public Guid NewPlanId { get; set; }

    public DateTime RequestedOn { get; set; }

    public DateOnly EffectiveDate { get; set; }

    public string? Reason { get; set; }

    public byte Status { get; set; }

    public Guid? ApprovedByUserId { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User? ApprovedByUser { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual SubscriptionPlan NewPlan { get; set; } = null!;

    public virtual SubscriptionPlan PreviousPlan { get; set; } = null!;

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
