using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CustomerContact
{
    public Guid CustomerContactId { get; set; }

    public Guid CustomerId { get; set; }

    public string ContactName { get; set; } = null!;

    public string? Designation { get; set; }

    public string? Email { get; set; }

    public string? MobileNumber { get; set; }

    public string? AlternateNumber { get; set; }

    public bool IsPrimaryContact { get; set; }

    public bool IsBillingContact { get; set; }

    public bool IsTechnicalContact { get; set; }

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
