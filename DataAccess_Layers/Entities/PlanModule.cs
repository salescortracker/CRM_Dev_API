using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PlanModule
{
    public Guid PlanModuleId { get; set; }

    public Guid SubscriptionPlanId { get; set; }

    public string ModuleCode { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public bool IsEnabled { get; set; }

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
