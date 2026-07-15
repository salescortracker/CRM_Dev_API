using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class AddOn
{
    public Guid AddOnId { get; set; }

    public Guid ProductId { get; set; }

    public string AddOnCode { get; set; } = null!;

    public string AddOnName { get; set; } = null!;

    public byte AddOnType { get; set; }

    public string? Description { get; set; }

    public bool IsRecurring { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<AddOnPricing> AddOnPricings { get; set; } = new List<AddOnPricing>();

    public virtual ICollection<CustomerAddOn> CustomerAddOns { get; set; } = new List<CustomerAddOn>();

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<QuotationItem> QuotationItems { get; set; } = new List<QuotationItem>();

    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<SubscriptionItem> SubscriptionItems { get; set; } = new List<SubscriptionItem>();
}
