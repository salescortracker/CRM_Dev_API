using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class DiscountType
{
    public int DiscountTypeId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string DiscountTypeName { get; set; } = null!;

    public string DiscountTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
