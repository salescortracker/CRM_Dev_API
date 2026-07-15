using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OpportunityActivity
{
    public Guid OpportunityActivityId { get; set; }

    public Guid OpportunityId { get; set; }

    public byte ActivityType { get; set; }

    public string Subject { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ActivityDate { get; set; }

    public Guid PerformedByUserId { get; set; }

    public DateTime? NextActionDate { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Opportunity Opportunity { get; set; } = null!;

    public virtual User PerformedByUser { get; set; } = null!;
}
