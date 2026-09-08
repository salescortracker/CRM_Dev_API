using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OpportunityStage1
{
    public int OpportunityStageId { get; set; }

    public string StageName { get; set; } = null!;

    public string StageCode { get; set; } = null!;

    public int Probability { get; set; }

    public int StageOrder { get; set; }

    public string ForecastCategory { get; set; } = null!;

    public string StageType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool WonStage { get; set; }

    public bool LostStage { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
