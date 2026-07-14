using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? CompanyCode { get; set; }

    public string? IndustryType { get; set; }

    public string? Headquarters { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? IsDefault { get; set; }

    public int? UserId { get; set; }

    public int? PlanId { get; set; }

    public DateTime? PlanStartDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? CompanyEmail { get; set; }

    public string? CompanyContact { get; set; }

    public string? CompanyAddress { get; set; }

    public string? CompanyLogo { get; set; }

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
