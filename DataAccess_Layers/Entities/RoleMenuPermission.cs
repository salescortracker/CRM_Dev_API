using System;

namespace DataAccess_Layers.Entities;

public partial class RoleMenuPermission
{
    public int RoleMenuPermissionId { get; set; }

    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public bool IsAllowed { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
