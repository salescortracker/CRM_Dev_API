using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PlanUserLimit
{
    public Guid PlanUserLimitId { get; set; }

    public Guid SubscriptionPlanId { get; set; }

    public int IncludedUserCount { get; set; }

    public int? MaximumUserCount { get; set; }

    public decimal AdditionalUserPrice { get; set; }

    public byte BillingCycle { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}
