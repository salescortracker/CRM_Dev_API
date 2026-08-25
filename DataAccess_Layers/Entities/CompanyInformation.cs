using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CompanyInformation
{
    public int CompanyInformationId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? LegalCompanyName { get; set; }

    public int IndustryId { get; set; }

    public int CompanyTypeId { get; set; }

    public string CompanyOwner { get; set; } = null!;

    public string CompanyStatus { get; set; } = null!;

    public string? Website { get; set; }

    public string? CompanyPhone { get; set; }

    public string? CompanyEmail { get; set; }

    public string? CompanyDescription { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = null!;

    public int StateId { get; set; }

    public int CountryId { get; set; }

    public string? PostalCode { get; set; }

    public string? NumberOfEmployees { get; set; }

    public decimal? AnnualRevenue { get; set; }

    public string? Gstnumber { get; set; }

    public string? Pannumber { get; set; }

    public string? CinregistrationNumber { get; set; }

    public string? LinkedInCompanyUrl { get; set; }

    public string? PrimaryContactName { get; set; }

    public string? PrimaryContactDesignation { get; set; }

    public string? PrimaryContactEmail { get; set; }

    public string? PrimaryContactPhone { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual CompanyType CompanyType { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Industry Industry { get; set; } = null!;

    public virtual StateMaster State { get; set; } = null!;
}
