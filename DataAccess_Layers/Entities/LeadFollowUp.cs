using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadFollowUp
{
    public Guid LeadFollowUpId { get; set; }

    public Guid LeadId { get; set; }

    public Guid AssignedToUserId { get; set; }

    public byte FollowUpType { get; set; }

    public DateTime FollowUpDate { get; set; }

    public DateTime? ReminderOn { get; set; }

    public byte Status { get; set; }

    public string? Remarks { get; set; }

    public DateTime? CompletedOn { get; set; }

    public Guid? CompletedByUserId { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual User AssignedToUser { get; set; } = null!;

    public virtual User? CompletedByUser { get; set; }

    public virtual Lead Lead { get; set; } = null!;
}
