using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PipelineSetting
{
    public int PipelineSettingId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string PipelineName { get; set; } = null!;

    public string PipelineCode { get; set; } = null!;

    public string PipelineType { get; set; } = null!;

    public int TotalStages { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
