using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadType
{
    public int LeadTypeId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string LeadTypeName { get; set; } = null!;

    public string LeadTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<LeadInformation> LeadInformations { get; set; } = new List<LeadInformation>();
}
