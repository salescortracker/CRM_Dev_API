using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ProjectTask
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public int ProjectId { get; set; }

    public int? MilestoneId { get; set; }

    public string AssignedTo { get; set; } = null!;

    public int PriorityId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public decimal? EstimatedHours { get; set; }

    public decimal? ActualHours { get; set; }

    public decimal CompletionPercentage { get; set; }

    public string? Tags { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ProjectMilestone? Milestone { get; set; }

    public virtual Priority Priority { get; set; } = null!;

    public virtual ProjectManagement Project { get; set; } = null!;

    public virtual ICollection<TimesheetManagement> TimesheetManagements { get; set; } = new List<TimesheetManagement>();
}
