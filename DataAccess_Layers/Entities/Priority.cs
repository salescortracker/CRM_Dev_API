using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Priority
{
    public int PriorityId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string PriorityName { get; set; } = null!;

    public string PriorityCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
