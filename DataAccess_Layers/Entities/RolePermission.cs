using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class RolePermission
{
    public int RolePermissionId { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public bool? CanView { get; set; }

    public bool? CanAdd { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }

    public bool? CanApprove { get; set; }

    public bool? CanExport { get; set; }

    public bool? Status { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? UserId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
