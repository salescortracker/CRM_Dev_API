using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Team
{
    public Guid TeamId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid? BranchId { get; set; }

    public Guid? DepartmentId { get; set; }

    public string TeamCode { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public Guid? TeamLeadUserId { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Department1? Department { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
