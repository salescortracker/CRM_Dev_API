using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class SubscriptionRenewal
{
    public Guid SubscriptionRenewalId { get; set; }

    public Guid SubscriptionId { get; set; }

    public Guid? RenewalQuotationId { get; set; }

    public Guid? RenewalInvoiceId { get; set; }

    public DateOnly RenewalDate { get; set; }

    public DateOnly PreviousEndDate { get; set; }

    public DateOnly NewEndDate { get; set; }

    public decimal RenewalAmount { get; set; }

    public byte Status { get; set; }

    public Guid? AssignedSalesUserId { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User? AssignedSalesUser { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Invoice? RenewalInvoice { get; set; }

    public virtual Quotation? RenewalQuotation { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
