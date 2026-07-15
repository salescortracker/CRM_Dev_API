using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Product
{
    public Guid ProductId { get; set; }

    public Guid? OrganizationId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public byte ProductType { get; set; }

    public string? Description { get; set; }

    public bool IsSubscriptionProduct { get; set; }

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

    public virtual ICollection<AddOn> AddOns { get; set; } = new List<AddOn>();

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<QuotationItem> QuotationItems { get; set; } = new List<QuotationItem>();

    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<SubscriptionItem> SubscriptionItems { get; set; } = new List<SubscriptionItem>();

    public virtual ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = new List<SubscriptionPlan>();
}
