using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadAssignment
{
    public Guid LeadAssignmentId { get; set; }

    public Guid LeadId { get; set; }

    public Guid AssignedToUserId { get; set; }

    public Guid AssignedByUserId { get; set; }

    public DateTime AssignedOn { get; set; }

    public DateTime? UnassignedOn { get; set; }

    public string? AssignmentReason { get; set; }

    public bool IsCurrentAssignment { get; set; }

    public virtual User AssignedByUser { get; set; } = null!;

    public virtual User AssignedToUser { get; set; } = null!;

    public virtual Lead Lead { get; set; } = null!;
}
