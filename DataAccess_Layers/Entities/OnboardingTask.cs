using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OnboardingTask
{
    public Guid OnboardingTaskId { get; set; }

    public Guid OnboardingProjectId { get; set; }

    public string TaskName { get; set; } = null!;

    public string? TaskDescription { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateTime? CompletedOn { get; set; }

    public byte Status { get; set; }

    public int DisplayOrder { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User? AssignedToUser { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual OnboardingProject OnboardingProject { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
