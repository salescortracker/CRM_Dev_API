using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CompanySubscription
{
    public int SubscriptionId { get; set; }

    public int OrganizationId { get; set; }

    public int PlanId { get; set; }

    public string Status { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public DateTime StartedDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public decimal Amount { get; set; }

    public int Seats { get; set; }

    public bool AutoRenew { get; set; }

    public string BillingCycle { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual OrganizationDatum Organization { get; set; } = null!;

    public virtual SubscriptionPlanMaster Plan { get; set; } = null!;
}
