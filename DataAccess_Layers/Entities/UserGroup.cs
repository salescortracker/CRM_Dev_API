using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class UserGroup
{
    public int UserGroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public string GroupCode { get; set; } = null!;

    public string? GroupType { get; set; }

    public int? DepartmentId { get; set; }

    public string? Team { get; set; }

    public string? ReportingManager { get; set; }

    public string? DefaultRole { get; set; }

    public int? UserLimit { get; set; }

    public int? PriorityId { get; set; }

    public bool Status { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Priority? Priority { get; set; }
}
