using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadActivity
{
    public Guid LeadActivityId { get; set; }

    public Guid LeadId { get; set; }

    public byte ActivityType { get; set; }

    public string Subject { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ActivityDate { get; set; }

    public Guid PerformedByUserId { get; set; }

    public string? Outcome { get; set; }

    public DateTime? NextFollowUpOn { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Lead Lead { get; set; } = null!;

    public virtual User PerformedByUser { get; set; } = null!;
}
