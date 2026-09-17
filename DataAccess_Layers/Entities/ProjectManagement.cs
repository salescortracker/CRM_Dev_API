using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ProjectManagement
{
    public int ProjectId { get; set; }

    public string ProjectCode { get; set; } = null!;

    public string ProjectName { get; set; } = null!;

    public string? Customer { get; set; }

    public int? ProjectManager { get; set; }

    public string? ProjectType { get; set; }

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? Budget { get; set; }

    public decimal CompletionPercentage { get; set; }

    public string? TeamMembers { get; set; }

    public string? ProjectDescription { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; } = new List<ProjectDocument>();

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    public virtual ICollection<TimesheetManagement> TimesheetManagements { get; set; } = new List<TimesheetManagement>();
}
