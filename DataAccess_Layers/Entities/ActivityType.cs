using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ActivityType
{
    public int ActivityTypeId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string ActivityTypeName { get; set; } = null!;

    public string ActivityTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
