using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CustomerAddOn
{
    public Guid CustomerAddOnId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid? SubscriptionId { get; set; }

    public Guid AddOnId { get; set; }

    public decimal Quantity { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public byte Status { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual AddOn AddOn { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
