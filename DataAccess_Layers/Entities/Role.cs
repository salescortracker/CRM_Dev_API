using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleCode { get; set; } = null!;

    public string RoleType { get; set; } = null!;

    public int HierarchyLevel { get; set; }

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? UserId { get; set; }
}
