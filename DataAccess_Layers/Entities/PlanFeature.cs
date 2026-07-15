using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PlanFeature
{
    public Guid PlanFeatureId { get; set; }

    public Guid SubscriptionPlanId { get; set; }

    public string FeatureName { get; set; } = null!;

    public string FeatureCode { get; set; } = null!;

    public string? FeatureValue { get; set; }

    public bool IsEnabled { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}
