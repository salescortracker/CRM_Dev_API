using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class AccessPolicy
{
    public int PolicyId { get; set; }

    public string PolicyName { get; set; } = null!;

    public string PolicyCode { get; set; } = null!;

    public int CompanyId { get; set; }

    public int? RegionId { get; set; }

    public string RoleName { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public string? Modules { get; set; }

    public bool IpRestriction { get; set; }

    public string? AllowedIp { get; set; }

    public string LoginRestriction { get; set; } = null!;

    public int SessionTimeout { get; set; }

    public bool Status { get; set; }

    public bool IsDefault { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Department? Department { get; set; }

    public virtual Region? Region { get; set; }
}
