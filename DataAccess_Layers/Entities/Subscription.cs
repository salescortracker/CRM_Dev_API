using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Subscription
{
    public Guid SubscriptionId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid SubscriptionPlanId { get; set; }

    public Guid? SalesOrderId { get; set; }

    public string SubscriptionNumber { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public byte BillingCycle { get; set; }

    public byte Status { get; set; }

    public int IncludedUsers { get; set; }

    public int AdditionalUsers { get; set; }

    public int TotalUserLimit { get; set; }

    public decimal IncludedStorageGb { get; set; }

    public decimal AdditionalStorageGb { get; set; }

    public decimal TotalStorageLimitGb { get; set; }

    public bool AutoRenew { get; set; }

    public int GracePeriodDays { get; set; }

    public DateTime? ActivatedOn { get; set; }

    public DateTime? CancelledOn { get; set; }

    public string? CancellationReason { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<CustomerTenant> CustomerTenants { get; set; } = new List<CustomerTenant>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<OnboardingProject> OnboardingProjects { get; set; } = new List<OnboardingProject>();

    public virtual SalesOrder? SalesOrder { get; set; }

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngrades { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionItem> SubscriptionItems { get; set; } = new List<SubscriptionItem>();

    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewals { get; set; } = new List<SubscriptionRenewal>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgrades { get; set; } = new List<SubscriptionUpgrade>();

    public virtual ICollection<SubscriptionUsage> SubscriptionUsages { get; set; } = new List<SubscriptionUsage>();

    public virtual User User { get; set; } = null!;
}
