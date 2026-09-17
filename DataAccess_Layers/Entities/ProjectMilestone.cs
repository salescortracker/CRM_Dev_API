using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ProjectMilestone
{
    public int MilestoneId { get; set; }

    public string MilestoneName { get; set; } = null!;

    public string Project { get; set; } = null!;

    public int? Owner { get; set; }

    public DateOnly? DueDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal CompletionPercentage { get; set; }

    public decimal? EstimatedHours { get; set; }

    public decimal? ActualHours { get; set; }

    public string Priority { get; set; } = null!;

    public DateOnly? TargetDate { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
