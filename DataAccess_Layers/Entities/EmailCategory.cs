using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class EmailCategory
{
    public int EmailCategoryId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string EmailCategoryName { get; set; } = null!;

    public string EmailCategoryCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
