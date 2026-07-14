using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Quotation
{
    public Guid QuotationId { get; set; }

    public Guid OrganizationId { get; set; }

    public string QuotationNumber { get; set; } = null!;

    public Guid OpportunityId { get; set; }

    public Guid? CustomerId { get; set; }

    public DateOnly QuotationDate { get; set; }

    public DateOnly ValidUntil { get; set; }

    public byte BillingCycle { get; set; }

    public decimal SubTotal { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public byte Status { get; set; }

    public int VersionNumber { get; set; }

    public string? TermsAndConditions { get; set; }

    public string? Notes { get; set; }

    public Guid CreatedByUserId { get; set; }

    public DateTime? SentOn { get; set; }

    public DateTime? AcceptedOn { get; set; }

    public DateTime? RejectedOn { get; set; }

    public string? RejectionReason { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Opportunity Opportunity { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<QuotationApproval> QuotationApprovals { get; set; } = new List<QuotationApproval>();

    public virtual ICollection<QuotationItem> QuotationItems { get; set; } = new List<QuotationItem>();

    public virtual ICollection<QuotationVersion> QuotationVersions { get; set; } = new List<QuotationVersion>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewals { get; set; } = new List<SubscriptionRenewal>();

    public virtual User User { get; set; } = null!;
}
