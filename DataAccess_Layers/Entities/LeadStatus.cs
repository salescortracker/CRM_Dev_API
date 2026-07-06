using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadStatus
{
    public Guid LeadStatusId { get; set; }

    public Guid OrganizationId { get; set; }

    public string StatusName { get; set; } = null!;

    public string StatusCode { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public byte StatusCategory { get; set; }

    public bool IsFinalStatus { get; set; }

    public bool IsDefault { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }
}
