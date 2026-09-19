using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class InvoiceMaster
{
    public int InvoiceId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public int OrganizationId { get; set; }

    public string? CompanyEmail { get; set; }

    public string? CompanyPhone { get; set; }

    public int PlanId { get; set; }

    public string BillingCycle { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public decimal GstPercentage { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal BalanceAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    public string Currency { get; set; } = null!;

    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual OrganizationDatum Organization { get; set; } = null!;

    public virtual SubscriptionPlanMaster Plan { get; set; } = null!;
}
