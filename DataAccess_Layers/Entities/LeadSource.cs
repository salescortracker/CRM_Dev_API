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

    public virtual Organization Organization { get; set; } = null!;
}
