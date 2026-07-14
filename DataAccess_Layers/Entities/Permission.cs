using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Permission
{
    public Guid PermissionId { get; set; }

    public string ModuleName { get; set; } = null!;

    public string ScreenName { get; set; } = null!;

    public string PermissionCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool CanView { get; set; }

    public bool CanAdd { get; set; }

    public bool CanEdit { get; set; }

    public bool CanDelete { get; set; }

    public bool CanApprove { get; set; }

    public bool CanExport { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
