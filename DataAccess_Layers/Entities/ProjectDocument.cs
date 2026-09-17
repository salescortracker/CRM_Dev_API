using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ProjectDocument
{
    public int DocumentId { get; set; }

    public string DocumentName { get; set; } = null!;

    public int ProjectId { get; set; }

    public string? Category { get; set; }

    public string? Version { get; set; }

    public int? UploadedBy { get; set; }

    public DateTime UploadDate { get; set; }

    public string? UploadFile { get; set; }

    public decimal? FileSizeKb { get; set; }

    public string? FileType { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ProjectManagement Project { get; set; } = null!;
}
