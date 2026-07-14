using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class SubscriptionPlan
{
    public Guid SubscriptionPlanId { get; set; }

    public Guid ProductId { get; set; }

    public string PlanCode { get; set; } = null!;

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public byte BillingCycle { get; set; }

    public decimal BasePrice { get; set; }

    public int IncludedUsers { get; set; }

    public decimal IncludedStorageGb { get; set; }

    public int TrialDays { get; set; }

    public int? ApirequestLimit { get; set; }

    public int? CallingMinutesLimit { get; set; }

    public int? Smslimit { get; set; }

    public int? WhatsAppLimit { get; set; }

    public bool IsPopular { get; set; }

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

    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = new List<PlanFeature>();

    public virtual ICollection<PlanModule> PlanModules { get; set; } = new List<PlanModule>();

    public virtual ICollection<PlanPricing> PlanPricings { get; set; } = new List<PlanPricing>();

    public virtual ICollection<PlanStorageLimit> PlanStorageLimits { get; set; } = new List<PlanStorageLimit>();

    public virtual ICollection<PlanUserLimit> PlanUserLimits { get; set; } = new List<PlanUserLimit>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngradeNewPlans { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngradePreviousPlans { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgradeNewPlans { get; set; } = new List<SubscriptionUpgrade>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgradePreviousPlans { get; set; } = new List<SubscriptionUpgrade>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
