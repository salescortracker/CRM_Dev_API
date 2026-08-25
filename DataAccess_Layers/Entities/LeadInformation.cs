using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LeadInformation
{
    public int LeadId { get; set; }

    public string LeadNumber { get; set; } = null!;

    public string? Salutation { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? JobTitle { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public int LeadTypeId { get; set; }

    public int? LeadOwnerId { get; set; }

    public int LeadSourceId { get; set; }

    public string LeadStatus { get; set; } = null!;

    public string? LeadRating { get; set; }

    public int? LeadScore { get; set; }

    public string? PreferredContactMethod { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Website { get; set; }

    public int? IndustryId { get; set; }

    public string? CompanySize { get; set; }

    public decimal? AnnualRevenue { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public int? StateId { get; set; }

    public string? PostalCode { get; set; }

    public int? CountryId { get; set; }

    public decimal? EstimatedDealValue { get; set; }

    public DateOnly? ExpectedCloseDate { get; set; }

    public string? Description { get; set; }

    public int? CompanyId { get; set; }

    public int? PrimaryContactId { get; set; }

    public int CrmcompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Industry? Industry { get; set; }

    public virtual LeadSourceDatum LeadSource { get; set; } = null!;

    public virtual LeadType LeadType { get; set; } = null!;

    public virtual StateMaster? State { get; set; }
}
