using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class QuotationVersion
{
    public Guid QuotationVersionId { get; set; }

    public Guid QuotationId { get; set; }

    public int VersionNumber { get; set; }

    public DateTime VersionDate { get; set; }

    public string? ChangeReason { get; set; }

    public string SnapshotJson { get; set; } = null!;

    public Guid CreatedByUserId { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Quotation Quotation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
