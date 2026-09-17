using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class KnowledgeBase
{
    public int ArticleId { get; set; }

    public string ArticleTitle { get; set; } = null!;

    public string? Category { get; set; }

    public string Status { get; set; } = null!;

    public string? Keywords { get; set; }

    public string Visibility { get; set; } = null!;

    public string? Author { get; set; }

    public string Version { get; set; } = null!;

    public DateTime? LastUpdated { get; set; }

    public string? Attachment { get; set; }

    public string? UploadType { get; set; }

    public string? Summary { get; set; }

    public string? ArticleContent { get; set; }

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }
}
