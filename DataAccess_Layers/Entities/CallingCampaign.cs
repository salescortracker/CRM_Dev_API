using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CallingCampaign
{
    public Guid CallingCampaignId { get; set; }

    public Guid OrganizationId { get; set; }

    public string CampaignName { get; set; } = null!;

    public string CampaignCode { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public Guid? AssignedTeamId { get; set; }

    public Guid? AssignedUserId { get; set; }

    public byte Status { get; set; }

    public int? TargetCalls { get; set; }

    public int? TargetDemos { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Team? AssignedTeam { get; set; }

    public virtual User? AssignedUser { get; set; }

    public virtual ICollection<CallingCampaignLead> CallingCampaignLeads { get; set; } = new List<CallingCampaignLead>();

    public virtual Organization Organization { get; set; } = null!;
}
