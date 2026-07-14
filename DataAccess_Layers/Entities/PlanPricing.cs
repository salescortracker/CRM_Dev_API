using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PlanPricing
{
    public Guid PlanPricingId { get; set; }

    public Guid SubscriptionPlanId { get; set; }

    public byte BillingCycle { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal TaxPercentage { get; set; }

    public DateOnly EffectiveFrom { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    public byte Status { get; set; }

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
