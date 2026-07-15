using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Invoice
{
    public Guid InvoiceId { get; set; }

    public Guid OrganizationId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public Guid? SalesOrderId { get; set; }

    public Guid CustomerId { get; set; }

    public byte InvoiceType { get; set; }

    public DateOnly InvoiceDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateOnly? BillingPeriodStart { get; set; }

    public DateOnly? BillingPeriodEnd { get; set; }

    public decimal SubTotal { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal BalanceAmount { get; set; }

    public byte InvoiceStatus { get; set; }

    public string? PaymentTerms { get; set; }

    public string? Notes { get; set; }

    public Guid GeneratedByUserId { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CreditNote> CreditNotes { get; set; } = new List<CreditNote>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual User GeneratedByUser { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual SalesOrder? SalesOrder { get; set; }

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewals { get; set; } = new List<SubscriptionRenewal>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgrades { get; set; } = new List<SubscriptionUpgrade>();

    public virtual User User { get; set; } = null!;
}
