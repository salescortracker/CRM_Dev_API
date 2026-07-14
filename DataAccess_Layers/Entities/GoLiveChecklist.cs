using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class GoLiveChecklist
{
    public Guid GoLiveChecklistId { get; set; }

    public Guid OnboardingProjectId { get; set; }

    public string ChecklistItem { get; set; } = null!;

    public bool IsMandatory { get; set; }

    public bool IsCompleted { get; set; }

    public Guid? CompletedByUserId { get; set; }

    public DateTime? CompletedOn { get; set; }

    public string? Remarks { get; set; }

    public int DisplayOrder { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User? CompletedByUser { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual OnboardingProject OnboardingProject { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
