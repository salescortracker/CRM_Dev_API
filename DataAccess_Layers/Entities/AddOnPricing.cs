using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class AddOnPricing
{
    public Guid AddOnPricingId { get; set; }

    public Guid AddOnId { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public byte UnitType { get; set; }

    public decimal UnitPrice { get; set; }

    public byte? BillingCycle { get; set; }

    public decimal TaxPercentage { get; set; }

    public DateOnly EffectiveFrom { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual AddOn AddOn { get; set; } = null!;
}
