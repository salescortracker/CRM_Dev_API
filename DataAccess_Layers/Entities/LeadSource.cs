using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadSource
{
    public Guid LeadSourceId { get; set; }

    public Guid OrganizationId { get; set; }

    public string SourceName { get; set; } = null!;

    public string SourceCode { get; set; } = null!;

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
