using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Opportunity
{
    public Guid OpportunityId { get; set; }

    public Guid OrganizationId { get; set; }

    public string OpportunityNumber { get; set; } = null!;

    public Guid? LeadId { get; set; }

    public Guid? CustomerId { get; set; }

    public string OpportunityName { get; set; } = null!;

    public Guid OwnerUserId { get; set; }

    public Guid OpportunityStageId { get; set; }

    public decimal ExpectedAmount { get; set; }

    public decimal ProbabilityPercentage { get; set; }

    public DateOnly? ExpectedCloseDate { get; set; }

    public DateOnly? ActualCloseDate { get; set; }

    public Guid? SubscriptionPlanId { get; set; }

    public int? RequiredUsers { get; set; }

    public decimal? RequiredStorageGb { get; set; }

    public string? RequirementDetails { get; set; }

    public string? LostReason { get; set; }

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

    public virtual Customer? Customer { get; set; }

    public virtual Lead? Lead { get; set; }

    public virtual ICollection<OpportunityActivity> OpportunityActivities { get; set; } = new List<OpportunityActivity>();

    public virtual ICollection<OpportunityProduct> OpportunityProducts { get; set; } = new List<OpportunityProduct>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual User OwnerUser { get; set; } = null!;

    public virtual ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();
}
