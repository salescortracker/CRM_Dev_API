using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PaymentTracking
{
    public int PaymentId { get; set; }

    public int OrganizationId { get; set; }

    public int PlanId { get; set; }

    public string? InvoiceNo { get; set; }

    public string? TransactionId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? Gateway { get; set; }

    public string Status { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public DateTime? NextRenewal { get; set; }

    public decimal? RefundAmount { get; set; }

    public string? RefundReason { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual OrganizationDatum Organization { get; set; } = null!;

    public virtual SubscriptionPlanMaster Plan { get; set; } = null!;
}
