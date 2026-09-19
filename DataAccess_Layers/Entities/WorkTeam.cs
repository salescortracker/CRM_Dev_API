using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class WorkTeam
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string TeamCode { get; set; } = null!;

    public int CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? TeamLeadId { get; set; }

    public string? Description { get; set; }

    public bool Status { get; set; }

    public bool IsDefault { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Department? Department { get; set; }

    public virtual Region? Region { get; set; }

    public virtual CompanyAdministrator? TeamLead { get; set; }

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
