using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class SalesOrder
{
    public Guid SalesOrderId { get; set; }

    public Guid OrganizationId { get; set; }

    public string SalesOrderNumber { get; set; } = null!;

    public Guid QuotationId { get; set; }

    public Guid CustomerId { get; set; }

    public DateOnly OrderDate { get; set; }

    public byte OrderStatus { get; set; }

    public decimal SubTotal { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public int? PaymentTermsDays { get; set; }

    public Guid CreatedByUserId { get; set; }

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

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual Quotation Quotation { get; set; } = null!;

    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual User User { get; set; } = null!;
}
