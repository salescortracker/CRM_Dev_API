using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OpportunityStage
{
    public Guid OpportunityStageId { get; set; }

    public Guid OrganizationId { get; set; }

    public string StageName { get; set; } = null!;

    public string StageCode { get; set; } = null!;

    public decimal ProbabilityPercentage { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsWonStage { get; set; }

    public bool IsLostStage { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
