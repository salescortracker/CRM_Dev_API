using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CrmmoduleConfigurationIndustry
{
    public int IndustryId { get; set; }

    public string IndustryName { get; set; } = null!;

    public string IndustryCode { get; set; } = null!;

    public string IndustryCategory { get; set; } = null!;

    public int CustomerCount { get; set; }

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
