using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadNote
{
    public Guid LeadNoteId { get; set; }

    public Guid LeadId { get; set; }

    public string NoteText { get; set; } = null!;

    public Guid CreatedByUserId { get; set; }

    public bool IsImportant { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual Lead Lead { get; set; } = null!;
}
