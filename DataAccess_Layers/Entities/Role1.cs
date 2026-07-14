using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Role1
{
    public Guid RoleId { get; set; }

    public Guid? OrganizationId { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsSystemRole { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
