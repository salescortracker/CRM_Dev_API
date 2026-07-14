using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Lead
{
    public Guid LeadId { get; set; }

    public Guid OrganizationId { get; set; }

    public string LeadNumber { get; set; } = null!;

    public byte LeadType { get; set; }

    public string? CompanyName { get; set; }

    public string ContactPersonName { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? MobileNumber { get; set; }

    public string? AlternateMobileNumber { get; set; }

    public string? Website { get; set; }

    public string? Designation { get; set; }

    public string? Industry { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public Guid? LeadSourceId { get; set; }

    public Guid LeadStatusId { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public Guid? AssignedByUserId { get; set; }

    public byte Priority { get; set; }

    public decimal? ExpectedValue { get; set; }

    public string? RequirementSummary { get; set; }

    public DateTime? NextFollowUpOn { get; set; }

    public bool IsConverted { get; set; }

    public Guid? ConvertedCustomerId { get; set; }

    public DateTime? ConvertedOn { get; set; }

    public string? LostReason { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual User? AssignedByUser { get; set; }

    public virtual User? AssignedToUser { get; set; }

    public virtual ICollection<CallingCampaignLead> CallingCampaignLeads { get; set; } = new List<CallingCampaignLead>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<LeadActivity> LeadActivities { get; set; } = new List<LeadActivity>();

    public virtual ICollection<LeadAssignment> LeadAssignments { get; set; } = new List<LeadAssignment>();

    public virtual ICollection<LeadCall> LeadCalls { get; set; } = new List<LeadCall>();

    public virtual ICollection<LeadFollowUp> LeadFollowUps { get; set; } = new List<LeadFollowUp>();

    public virtual ICollection<LeadNote> LeadNotes { get; set; } = new List<LeadNote>();

    public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();

    public virtual ICollection<TwilioSmslog> TwilioSmslogs { get; set; } = new List<TwilioSmslog>();
}
