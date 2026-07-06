using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Department1
{
    public Guid DepartmentId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid? BranchId { get; set; }

    public string DepartmentCode { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public Guid? DepartmentHeadUserId { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
