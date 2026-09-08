using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CrmmoduleConfigurationSource
{
    public int SourceId { get; set; }

    public string SourceName { get; set; } = null!;

    public string SourceCode { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal ConversionRate { get; set; }

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
