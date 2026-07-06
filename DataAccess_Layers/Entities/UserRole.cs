using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class UserRole
{
    public Guid UserRoleId { get; set; }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public Guid? AssignedBy { get; set; }

    public DateTime AssignedOn { get; set; }

    public bool IsPrimaryRole { get; set; }

    public virtual User? AssignedByNavigation { get; set; }

    public virtual Role1 Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
