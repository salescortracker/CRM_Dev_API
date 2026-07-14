using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CustomerBillingDetail
{
    public Guid CustomerBillingDetailId { get; set; }

    public Guid CustomerId { get; set; }

    public string BillingCompanyName { get; set; } = null!;

    public string? Gstnumber { get; set; }

    public string? Pannumber { get; set; }

    public string? BillingEmail { get; set; }

    public string? BillingMobileNumber { get; set; }

    public int? PaymentTermsDays { get; set; }

    public decimal? CreditLimit { get; set; }

    public bool IsTaxExempt { get; set; }

    public string? TaxExemptionNumber { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
