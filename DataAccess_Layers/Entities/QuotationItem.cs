using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class QuotationItem
{
    public Guid QuotationItemId { get; set; }

    public Guid QuotationId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? AddOnId { get; set; }

    public byte ItemType { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxPercentage { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public byte? BillingCycle { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual AddOn? AddOn { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Quotation Quotation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
