using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PlanStorageLimit
{
    public Guid PlanStorageLimitId { get; set; }

    public Guid SubscriptionPlanId { get; set; }

    public decimal IncludedStorageGb { get; set; }

    public decimal? MaximumStorageGb { get; set; }

    public decimal AdditionalStoragePricePerGb { get; set; }

    public byte BillingCycle { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}
