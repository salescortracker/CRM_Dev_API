using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OpportunityProduct
{
    public Guid OpportunityProductId { get; set; }

    public Guid OpportunityId { get; set; }

    public Guid ProductId { get; set; }

    public string? Description { get; set; }

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal TaxPercentage { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Opportunity Opportunity { get; set; } = null!;
}
