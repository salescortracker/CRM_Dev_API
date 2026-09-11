using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CompanyProfile
{
    public int CompanyProfileId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string CompanyCode { get; set; } = null!;

    public string? LegalName { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Gstnumber { get; set; }

    public string? Pannumber { get; set; }

    public int? IndustryId { get; set; }

    public int? CompanyTypeId { get; set; }

    public DateOnly? EstablishedDate { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Website { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public string? City { get; set; }

    public string? Pincode { get; set; }

    public int? CurrencyId { get; set; }

    public string? FinancialYear { get; set; }

    public string? CompanyLogoPath { get; set; }

    public string? Description { get; set; }

    public bool Active { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual CompanyType? CompanyType { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Currency? Currency { get; set; }

    public virtual Industry? Industry { get; set; }

    public virtual StateMaster? State { get; set; }
}
