using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Billing
{
    public int BillingId { get; set; }

    public string BillNumber { get; set; } = null!;

    public int OrganizationId { get; set; }

    public int PlanId { get; set; }

    public DateTime BillingDate { get; set; }

    public DateTime? DueDate { get; set; }

    public decimal Amount { get; set; }

    public decimal Tax { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public string? BillingAddress { get; set; }

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual OrganizationDatum Organization { get; set; } = null!;

    public virtual SubscriptionPlanMaster Plan { get; set; } = null!;
}
